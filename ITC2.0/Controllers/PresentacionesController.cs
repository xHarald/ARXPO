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
    public class PresentacionesController : ControllerBase
    {
        private readonly ArxpoContext _context;

        public PresentacionesController(ArxpoContext context)
        {
            _context = context;
        }

        // GET: api/Presentaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PresentacionesMV>>> GetPresentacione()
        {
            if (_context.Presentaciones == null)
            {
                return NotFound();
            }
            var query = from presentaciones in await _context.Presentaciones.Where(e => e.Estado).ToListAsync()
                        join proyectos in await _context.Proyectos.ToListAsync() on presentaciones.IdProyecto equals proyectos.Id
                        join administradores in await _context.Administradores.ToListAsync() on presentaciones.IdAdministrador equals administradores.Id
                        select new PresentacionesMV
                        {
                            Codigo = presentaciones.Id,
                            Dia_Presentacion = presentaciones.DiaPresentacion,
                            Salon = presentaciones.Salon,
                            Nombre_Proyecto = proyectos.Nombre,
                            Administrador = administradores.Nombre
                        };
            return query.ToList();
        }

        // GET: api/Presentaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Presentacione>> GetPresentacione(int id)
        {
          if (_context.Presentaciones == null)
          {
              return NotFound();
          }
            var presentacione = await _context.Presentaciones.FindAsync(id);

            if (presentacione == null)
            {
                return NotFound();
            }

            return presentacione;
        }

        // PUT: api/Presentaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPresentacione(int id, Presentacione presentacione)
        {
            if (id != presentacione.Id)
            {
                return BadRequest();
            }

            _context.Entry(presentacione).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PresentacioneExists(id))
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

        // POST: api/Presentaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Presentacione>> PostPresentacione(Presentacione presentacione)
        {
          if (_context.Presentaciones == null)
          {
              return Problem("Entity set 'ArxpoContext.Presentaciones'  is null.");
          }
            _context.Presentaciones.Add(presentacione);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPresentacione", new { id = presentacione.Id }, presentacione);
        }

        // DELETE: api/Presentaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePresentacione(int id)
        {
            if (_context.Presentaciones == null)
            {
                return NotFound();
            }

            var presentacion = await _context.Presentaciones.FindAsync(id);
            if (presentacion == null)
            {
                return NotFound();
            }

            // En lugar de eliminar físicamente, marca el estudiante como inactivo
            presentacion.Estado = false;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PresentacioneExists(int id)
        {
            return (_context.Presentaciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
