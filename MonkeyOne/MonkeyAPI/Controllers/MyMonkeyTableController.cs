using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonkeyAPI.Models;
using Serilog;

namespace MonkeyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyMonkeyTableController : ControllerBase
    {
        private readonly MonkeyDbContext _context;

        public MyMonkeyTableController(MonkeyDbContext context)
        {
            _context = context;
        }

        // GET: api/MyMonkeyTable
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Monkeytable>>> GetMonkeytables()
        {
          if (_context.Monkeytables == null)
          {
              return NotFound();
          }
            return await _context.Monkeytables.ToListAsync();
        }

        // GET: api/MyMonkeyTable/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Monkeytable>> GetMonkeytable(int id)
        {
          if (_context.Monkeytables == null)
          {
              return NotFound();
          }
            var monkeytable = await _context.Monkeytables.FindAsync(id);

            if (monkeytable == null)
            {
                return NotFound();
            }

            return monkeytable;
        }

        // PUT: api/MyMonkeyTable/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonkeytable(int id, Monkeytable monkeytable)
        {
            if (id != monkeytable.Id)
            {
                return BadRequest();
            }

            _context.Entry(monkeytable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonkeytableExists(id))
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

        // POST: api/MyMonkeyTable
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Monkeytable>> PostMonkeytable(Monkeytable monkeytable)
        {

            Console.WriteLine("Monkeytable received by server: ", monkeytable);

            if (_context.Monkeytables == null)
          {
              return Problem("Entity set 'MonkeyDbContext.Monkeytables'  is null.");
          }
            _context.Monkeytables.Add(monkeytable);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MonkeytableExists(monkeytable.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMonkeytable", new { id = monkeytable.Id }, monkeytable);
        }

        // DELETE: api/MyMonkeyTable/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonkeytable(int id)
        {
            if (_context.Monkeytables == null)
            {
                return NotFound();
            }
            var monkeytable = await _context.Monkeytables.FindAsync(id);
            if (monkeytable == null)
            {
                return NotFound();
            }

            _context.Monkeytables.Remove(monkeytable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MonkeytableExists(int id)
        {
            return (_context.Monkeytables?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // Test-logging
        [HttpGet("test-logging")]
        public IActionResult TestLogging()
        {
            try
            {
                throw new Exception("This is a test exception to trigger logging");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while testing logging");
                return StatusCode(500);
            }
        }

    }
}
