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
    public class ProgramasController : ControllerBase
    {
        private readonly ArxpoContext _context;

        public ProgramasController(ArxpoContext context)
        {
            _context = context;
        }

        // GET: api/Programas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramasMV>>> GetPrograma()
        {
            if (_context.Programas == null)
            {
                return NotFound();
            }
            var query = from programas in await _context.Programas.Where(e => e.Estado).ToListAsync()
                        join facultades in await _context.Facultades.ToListAsync() on programas.IdFacultad equals facultades.Id
                        select new ProgramasMV
                        {
                            Codigo = programas.Id,
                            Nombre_Programa = programas.NombrePrograma,
                            Descripcion = programas.Descripcion,
                            Facultad = facultades.Nombre
                        };
            return query.ToList();
        }

        // GET: api/Programas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Programa>> GetPrograma(int id)
        {
          if (_context.Programas == null)
          {
              return NotFound();
          }
            var programa = await _context.Programas.FindAsync(id);

            if (programa == null)
            {
                return NotFound();
            }

            return programa;
        }

        // PUT: api/Programas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrograma(int id, Programa programa)
        {
            if (id != programa.Id)
            {
                return BadRequest();
            }

            _context.Entry(programa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramaExists(id))
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

        // POST: api/Programas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Programa>> PostPrograma(Programa programa)
        {
          if (_context.Programas == null)
          {
              return Problem("Entity set 'ArxpoContext.Programas'  is null.");
          }
            _context.Programas.Add(programa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrograma", new { id = programa.Id }, programa);
        }

        // DELETE: api/Programas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramas(int id)
        {
            if (_context.Programas == null)
            {
                return NotFound();
            }

            var programa = await _context.Programas.FindAsync(id);
            if (programa == null)
            {
                return NotFound();
            }

            // En lugar de eliminar físicamente, marca el estudiante como inactivo
            programa.Estado = false;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgramaExists(int id)
        {
            return (_context.Programas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
