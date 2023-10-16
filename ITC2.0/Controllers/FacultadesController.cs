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
    public class FacultadesController : ControllerBase
    {
        private readonly ArxpoContext _context;

        public FacultadesController(ArxpoContext context)
        {
            _context = context;
        }

        // GET: api/Facultades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacultadesMV>>> GetEstudiantes()
        {
            if (_context.Estudiantes == null)
            {
                return NotFound();
            }
            var query = from facultades in await _context.Facultades.ToListAsync()
                        select new FacultadesMV
                        {
                            Codigo = facultades.Id,
                            Nombre = facultades.Nombre,
                            Descripcion = facultades.Descripcion,
                            Contacto = facultades.TelefonoContacto,
                            Correo = facultades.Correo
                        };
            return query.ToList();
        }

        // GET: api/Facultades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Facultade>> GetFacultade(int id)
        {
          if (_context.Facultades == null)
          {
              return NotFound();
          }
            var facultade = await _context.Facultades.FindAsync(id);

            if (facultade == null)
            {
                return NotFound();
            }

            return facultade;
        }

        // PUT: api/Facultades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacultade(int id, Facultade facultade)
        {
            if (id != facultade.Id)
            {
                return BadRequest();
            }

            _context.Entry(facultade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacultadeExists(id))
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

        // POST: api/Facultades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Facultade>> PostFacultade(Facultade facultade)
        {
          if (_context.Facultades == null)
          {
              return Problem("Entity set 'ArxpoContext.Facultades'  is null.");
          }
            _context.Facultades.Add(facultade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacultade", new { id = facultade.Id }, facultade);
        }

        // DELETE: api/Facultades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacultade(int id)
        {
            if (_context.Facultades == null)
            {
                return NotFound();
            }
            var facultade = await _context.Facultades.FindAsync(id);
            if (facultade == null)
            {
                return NotFound();
            }

            _context.Facultades.Remove(facultade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacultadeExists(int id)
        {
            return (_context.Facultades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
