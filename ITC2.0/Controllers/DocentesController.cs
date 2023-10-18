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
    public class DocentesController : ControllerBase
    {
        private readonly ArxpoContext _context;

        public DocentesController(ArxpoContext context)
        {
            _context = context;
        }

        // GET: api/Docentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocentesMV>>> GetEstudiantes()
        {
            if (_context.Estudiantes == null)
            {
                return NotFound();
            }
            var query = from docentes in await _context.Docentes.ToListAsync()
                        join usuarios in await _context.Usuarios.ToListAsync() on docentes.IdUsuario equals usuarios.Id
                        join presentaciones in await _context.Presentaciones.ToListAsync() on docentes.IdPresentacion equals presentaciones.Id
                        select new DocentesMV
                        {
                            Codigo = docentes.Id,
                            Correo = usuarios.Correo,
                            Nombre = docentes.Nombre,
                            Identificacion = docentes.Identificacion,
                            Presentacion = presentaciones.Salon,
                        };
            return query.ToList();
        }

        // GET: api/Docentes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Docente>> GetDocente(int id)
        {
          if (_context.Docentes == null)
          {
              return NotFound();
          }
            var docente = await _context.Docentes.FindAsync(id);

            if (docente == null)
            {
                return NotFound();
            }

            return docente;
        }

        // PUT: api/Docentes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocente(int id, Docente docente)
        {
            if (id != docente.Id)
            {
                return BadRequest();
            }

            _context.Entry(docente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocenteExists(id))
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

        // POST: api/Docentes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Docente>> PostDocente(Docente docente)
        {
          if (_context.Docentes == null)
          {
              return Problem("Entity set 'ArxpoContext.Docentes'  is null.");
          }
            _context.Docentes.Add(docente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocente", new { id = docente.Id }, docente);
        }

        // DELETE: api/Docentes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocente(int id)
        {
            if (_context.Docentes == null)
            {
                return NotFound();
            }
            var docente = await _context.Docentes.FindAsync(id);
            if (docente == null)
            {
                return NotFound();
            }

            _context.Docentes.Remove(docente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocenteExists(int id)
        {
            return (_context.Docentes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
