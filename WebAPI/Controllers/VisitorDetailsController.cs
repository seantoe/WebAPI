using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorDetailsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VisitorDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/VisitorDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Visitor>>> GetVisitors()
        {
            return await _context.Visitors.ToListAsync();
        }

        // GET: api/VisitorDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Visitor>> GetVisitor(int id)
        {
            var visitor = await _context.Visitors.FindAsync(id);

            if (visitor == null)
            {
                return NotFound();
            }

            return visitor;
        }

        // PUT: api/VisitorDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitor(int id, Visitor visitor)
        {
            if (id != visitor.VisitorId)
            {
                return BadRequest();
            }

            _context.Entry(visitor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(await _context.Visitors.ToListAsync());
        }

        // POST: api/VisitorDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Visitor>> PostVisitor(Visitor visitor)
        {
            _context.Visitors.Add(visitor);
            await _context.SaveChangesAsync();

            return Ok(await _context.Visitors.ToListAsync());
        }

        // DELETE: api/VisitorDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(int id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor == null)
            {
                return NotFound();
            }

            _context.Visitors.Remove(visitor);
            await _context.SaveChangesAsync();

            return Ok(await _context.Visitors.ToListAsync());
        }

        private bool VisitorExists(int id)
        {
            return _context.Visitors.Any(e => e.VisitorId == id);
        }
    }
}
