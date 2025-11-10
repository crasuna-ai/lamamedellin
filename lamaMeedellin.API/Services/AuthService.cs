using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LAMAMedellin.API.Data;
using LAMAMedellin.API.DTOs;
using LAMAMedellin.API.Models;

namespace LAMAMedellin.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            ApplicationDbContext context,
            IConfiguration configuration,
            ILogger<AuthService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                // Verificar si el usuario ya existe
                if (await UserExistsAsync(registerDto.Email))
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "El usuario ya existe con ese email"
                    };
                }

                // Crear hash de la contraseña
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

                // Crear nuevo usuario
                var user = new Usuario
                {
                    Email = registerDto.Email,
                    PasswordHash = passwordHash,
                    NombreCompleto = registerDto.NombreCompleto,
                    Rol = "Usuario",
                    Activo = true,
                    FechaRegistro = DateTime.Now
                };

                _context.Usuarios.Add(user);
                await _context.SaveChangesAsync();

                // Generar token
                var token = GenerateJwtToken(user);

                return new AuthResponseDto
                {
                    Success = true,
                    Message = "Usuario registrado exitosamente",
                    Token = token,
                    Expiration = DateTime.Now.AddMinutes(
                        Convert.ToInt32(_configuration["JwtSettings:ExpirationMinutes"])),
                    User = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        NombreCompleto = user.NombreCompleto,
                        Rol = user.Rol,
                        MiembroId = user.MiembroId
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar usuario");
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Error al registrar usuario"
                };
            }
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            try
            {
                // Buscar usuario por email
                var user = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

                if (user == null)
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Email o contraseña incorrectos"
                    };
                }

                // Verificar que el usuario esté activo
                if (!user.Activo)
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Usuario inactivo"
                    };
                }

                // Verificar contraseña
                bool passwordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

                if (!passwordValid)
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Email o contraseña incorrectos"
                    };
                }

                // Actualizar último acceso
                user.UltimoAcceso = DateTime.Now;
                await _context.SaveChangesAsync();

                // Generar token
                var token = GenerateJwtToken(user);

                return new AuthResponseDto
                {
                    Success = true,
                    Message = "Login exitoso",
                    Token = token,
                    Expiration = DateTime.Now.AddMinutes(
                        Convert.ToInt32(_configuration["JwtSettings:ExpirationMinutes"])),
                    User = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        NombreCompleto = user.NombreCompleto,
                        Rol = user.Rol,
                        MiembroId = user.MiembroId
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al hacer login");
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Error al hacer login"
                };
            }
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email);
        }

        private string GenerateJwtToken(Usuario user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey no configurado");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.NombreCompleto),
                new Claim(ClaimTypes.Role, user.Rol),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(jwtSettings["ExpirationMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}