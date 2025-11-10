using LAMAMedellin.API.Data;
using LAMAMedellin.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAMAMedellin.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MiembrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MiembrosController> _logger;

        public MiembrosController(ApplicationDbContext context, ILogger<MiembrosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Miembros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Miembro>>> GetMiembros()
        {
            try
            {
                var miembros = await _context.Miembros.ToListAsync();
                return Ok(miembros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los miembros");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/Miembros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Miembro>> GetMiembro(int id)
        {
            var miembro = await _context.Miembros.FindAsync(id);

            if (miembro == null)
            {
                return NotFound();
            }

            return Ok(miembro);
        }

        // POST: api/Miembros
        [HttpPost]
        public async Task<ActionResult<Miembro>> PostMiembro(Miembro miembro)
        {
            try
            {
                _context.Miembros.Add(miembro);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMiembro), new { id = miembro.Id }, miembro);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el miembro");
                return BadRequest("Error al crear el miembro");
            }
        }

        // PUT: api/Miembros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMiembro(int id, Miembro miembro)
        {
            if (id != miembro.Id)
            {
                return BadRequest("El ID no coincide");
            }

            _context.Entry(miembro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MiembroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Miembros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMiembro(int id)
        {
            var miembro = await _context.Miembros.FindAsync(id);
            if (miembro == null)
            {
                return NotFound();
            }

            _context.Miembros.Remove(miembro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MiembroExists(int id)
        {
            return _context.Miembros.Any(e => e.Id == id);
        }
    }
}