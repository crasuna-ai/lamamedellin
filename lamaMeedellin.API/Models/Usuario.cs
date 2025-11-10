using System.ComponentModel.DataAnnotations;

namespace LAMAMedellin.API.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Rol { get; set; } = "Usuario";

        public int? MiembroId { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public DateTime? UltimoAcceso { get; set; }

        // Navegación
        public Miembro? Miembro { get; set; }
    }
}