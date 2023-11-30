using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITC2._0.Models;
using ITC2._0.ModelsView;

namespace ITC2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetasController : ControllerBase
    {
        private readonly ArxpoContext _context;

        public TarjetasController(ArxpoContext context)
        {
            _context = context;
        }

        // GET: api/Tarjetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarjetasMV>>> GetTarjeta()
        {
            if (_context.Tarjetas == null)
            {
                return NotFound();
            }
            var query = from tarjetas in await _context.Tarjetas.Where(e => e.Estado == "A").ToListAsync()
                        select new TarjetasMV
                        {
                            Codigo = tarjetas.Id,
                            Titulo = tarjetas.Titulo,
                            Descripcion = tarjetas.Descripcion,
                            Link = tarjetas.Link,
                            Extension = tarjetas.Extension,
                            Observacion = tarjetas.Observacion,
                            Fecha_Subida = tarjetas.FechaSubida,
                            Fecha_Terminado = tarjetas.FechaTerminado,
                            Estado = tarjetas.EstadoTarjeta,
                        };
            return query.ToList();
        }

        // GET: api/Tarjetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarjeta>> GetTarjeta(int id)
        {
          if (_context.Tarjetas == null)
          {
              return NotFound();
          }
            var tarjeta = await _context.Tarjetas.FindAsync(id);

            if (tarjeta == null)
            {
                return NotFound();
            }

            return tarjeta;
        }

        // PUT: api/Tarjetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarjeta(int id, Tarjeta tarjeta)
        {
            if (id != tarjeta.Id)
            {
                return BadRequest();
            }

            _context.Entry(tarjeta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarjetaExists(id))
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

        // POST: api/Tarjetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tarjeta>> PostTarjeta(Tarjeta tarjeta)
        {
          if (_context.Tarjetas == null)
          {
              return Problem("Entity set 'ArxpoContext.Tarjetas'  is null.");
          }
            _context.Tarjetas.Add(tarjeta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarjeta", new { id = tarjeta.Id }, tarjeta);
        }

        // DELETE: api/Tarjetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarjeta(int id)
        {
            if (_context.Tarjetas == null)
            {
                return NotFound();
            }

            var tarjeta = await _context.Tarjetas.FindAsync(id);
            if (tarjeta == null)
            {
                return NotFound();
            }

            // En lugar de eliminar físicamente, marca el estudiante como inactivo
            tarjeta.Estado = "I";

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarjetaExists(int id)
        {
            return (_context.Tarjetas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
