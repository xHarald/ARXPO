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
    public class SupervisoresController : ControllerBase
    {
        private readonly ArxpoContext _context;

        public SupervisoresController(ArxpoContext context)
        {
            _context = context;
        }

        // GET: api/Supervisores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupervisoresMV>>> GetEstudiantes()
        {
            if (_context.Estudiantes == null)
            {
                return NotFound();
            }
            var query = from supervisores in await _context.Supervisores.ToListAsync()
                        join proyectos in await _context.Proyectos.ToListAsync() on supervisores.IdProyecto equals proyectos.Id
                        join docentes in await _context.Docentes.ToListAsync() on supervisores.IdDocente equals docentes.Id
                        select new SupervisoresMV
                        {
                            Codigo = supervisores.Id,
                            Nombre_Proyecto = proyectos.Nombre,
                            Motivo = supervisores.Motivo,
                            Nombre_Docente = docentes.Nombre,
                        };
            return query.ToList();
        }

        // GET: api/Supervisores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supervisore>> GetSupervisore(int id)
        {
          if (_context.Supervisores == null)
          {
              return NotFound();
          }
            var supervisore = await _context.Supervisores.FindAsync(id);

            if (supervisore == null)
            {
                return NotFound();
            }

            return supervisore;
        }

        // PUT: api/Supervisores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupervisore(int id, Supervisore supervisore)
        {
            if (id != supervisore.Id)
            {
                return BadRequest();
            }

            _context.Entry(supervisore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupervisoreExists(id))
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

        // POST: api/Supervisores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Supervisore>> PostSupervisore(Supervisore supervisore)
        {
          if (_context.Supervisores == null)
          {
              return Problem("Entity set 'ArxpoContext.Supervisores'  is null.");
          }
            _context.Supervisores.Add(supervisore);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupervisore", new { id = supervisore.Id }, supervisore);
        }

        // DELETE: api/Supervisores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupervisore(int id)
        {
            if (_context.Supervisores == null)
            {
                return NotFound();
            }
            var supervisore = await _context.Supervisores.FindAsync(id);
            if (supervisore == null)
            {
                return NotFound();
            }

            _context.Supervisores.Remove(supervisore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupervisoreExists(int id)
        {
            return (_context.Supervisores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
