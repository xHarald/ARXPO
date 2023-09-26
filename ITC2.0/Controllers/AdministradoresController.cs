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
    public class AdministradoresController : ControllerBase
    {
        private readonly ArxpoContext _context;

        public AdministradoresController(ArxpoContext context)
        {
            _context = context;
        }

        // GET: api/Administradores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administradore>>> GetAdministradores()
        {
          if (_context.Administradores == null)
          {
              return NotFound();
          }
            return await _context.Administradores.ToListAsync();
        }

        // GET: api/Administradores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Administradore>> GetAdministradore(int id)
        {
          if (_context.Administradores == null)
          {
              return NotFound();
          }
            var administradore = await _context.Administradores.FindAsync(id);

            if (administradore == null)
            {
                return NotFound();
            }

            return administradore;
        }

        // PUT: api/Administradores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministradore(int id, Administradore administradore)
        {
            if (id != administradore.Id)
            {
                return BadRequest();
            }

            _context.Entry(administradore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministradoreExists(id))
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

        // POST: api/Administradores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Administradore>> PostAdministradore(Administradore administradore)
        {
          if (_context.Administradores == null)
          {
              return Problem("Entity set 'ArxpoContext.Administradores'  is null.");
          }
            _context.Administradores.Add(administradore);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdministradore", new { id = administradore.Id }, administradore);
        }

        // DELETE: api/Administradores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministradore(int id)
        {
            if (_context.Administradores == null)
            {
                return NotFound();
            }
            var administradore = await _context.Administradores.FindAsync(id);
            if (administradore == null)
            {
                return NotFound();
            }

            _context.Administradores.Remove(administradore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdministradoreExists(int id)
        {
            return (_context.Administradores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
