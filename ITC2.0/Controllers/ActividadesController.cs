using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITC2._0.Models;

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
        public async Task<ActionResult<IEnumerable<Actividade>>> GetActividades()
        {
          if (_context.Actividades == null)
          {
              return NotFound();
          }
            return await _context.Actividades.ToListAsync();
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
            var actividade = await _context.Actividades.FindAsync(id);
            if (actividade == null)
            {
                return NotFound();
            }

            _context.Actividades.Remove(actividade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActividadeExists(int id)
        {
            return (_context.Actividades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
