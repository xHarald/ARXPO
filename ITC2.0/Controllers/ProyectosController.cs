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
    public class ProyectosController : ControllerBase
    {
        private readonly ArxpoContext _context;

        public ProyectosController(ArxpoContext context)
        {
            _context = context;
        }

        // GET: api/Proyectos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProyectosMV>>> GetProyecto()
        {
            if (_context.Proyectos == null)
            {
                return NotFound();
            }
            var query = from proyectos in await _context.Proyectos.Where(e => e.Estado).ToListAsync()
                        join tarjetas in await _context.Tarjetas.ToListAsync() on proyectos.IdTarjeta equals tarjetas.Id
                        select new ProyectosMV
                        {
                            Codigo = proyectos.Id,
                            Nombre = proyectos.Nombre,
                            Descripcion = proyectos.Descripcion,
                            Integrantes = proyectos.NumeroIntegrantes,
                            Ultima_Actualizacion = proyectos.UltimaActualizacion,
                            Estado = proyectos.EstadoProyecto,
                            Titulo = tarjetas.Titulo
                        };
            return query.ToList();
        }

        // GET: api/Proyectos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proyecto>> GetProyecto(int id)
        {
          if (_context.Proyectos == null)
          {
              return NotFound();
          }
            var proyecto = await _context.Proyectos.FindAsync(id);

            if (proyecto == null)
            {
                return NotFound();
            }

            return proyecto;
        }

        // PUT: api/Proyectos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProyecto(int id, Proyecto proyecto)
        {
            if (id != proyecto.Id)
            {
                return BadRequest();
            }

            _context.Entry(proyecto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProyectoExists(id))
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

        // POST: api/Proyectos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proyecto>> PostProyecto(Proyecto proyecto)
        {
          if (_context.Proyectos == null)
          {
              return Problem("Entity set 'ArxpoContext.Proyectos'  is null.");
          }
            _context.Proyectos.Add(proyecto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProyecto", new { id = proyecto.Id }, proyecto);
        }

        // DELETE: api/Proyectos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProyecto(int id)
        {
            if (_context.Proyectos == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }

            // En lugar de eliminar físicamente, marca el estudiante como inactivo
            proyecto.Estado = false;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProyectoExists(int id)
        {
            return (_context.Proyectos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
