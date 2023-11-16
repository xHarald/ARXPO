using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITC2._0.Models;
using ITC2._0.ModelsView;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ITC2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadesController : ControllerBase
    {
        private readonly ArxpoContext _context;

        public ActividadesController(ArxpoContext context)
        {
            _context = context;
        }

        // GET: api/Actividades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActividadesMV>>> GetActividade()
        {
            if (_context.Actividades == null)
            {
                return NotFound();
            }
            var query = from actividades in await _context.Actividades.Where(e => e.Estado).ToListAsync()
                        join estudiantes in await _context.Estudiantes.ToListAsync() on actividades.IdEstudiante equals estudiantes.Id
                        select new ActividadesMV
                        {
                            Codigo = actividades.Id,
                            Estudiante = estudiantes.Nombre,
                            Titulo = actividades.TituloActividad,
                            Descripcion = actividades.Descripcion,
                            Horas = actividades.Horas,
                            Terminar = actividades.Horas,
                        };
            return query.ToList();
        }


        // GET: api/Actividades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Actividade>> GetActividade(int id)
        {
          if (_context.Actividades == null)
          {
              return NotFound();
          }
            var actividade = await _context.Actividades.FindAsync(id);

            if (actividade == null)
            {
                return NotFound();
            }

            return actividade;
        }

        // PUT: api/Actividades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActividade(int id, Actividade actividade)
        {
            if (id != actividade.Id)
            {
                return BadRequest();
            }

            _context.Entry(actividade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActividadeExists(id))
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

        // POST: api/Actividades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Actividade>> PostActividade(Actividade actividade)
        {
          if (_context.Actividades == null)
          {
              return Problem("Entity set 'ArxpoContext.Actividades'  is null.");
          }
            _context.Actividades.Add(actividade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActividade", new { id = actividade.Id }, actividade);
        }

        // DELETE: api/Actividades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActividade(int id)
        {
            if (_context.Actividades == null)
            {
                return NotFound();
            }

            var actividad = await _context.Actividades.FindAsync(id);
            if (actividad == null)
            {
                return NotFound();
            }

            // En lugar de eliminar físicamente, marca el estudiante como inactivo
            actividad.Estado = false;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActividadeExists(int id)
        {
            return (_context.Actividades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
