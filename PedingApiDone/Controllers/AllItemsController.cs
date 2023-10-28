using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PedingApiDone.Models;

namespace PedingApiDone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllItemsController : ControllerBase
    {
        private readonly AllContext _context;

        public AllItemsController(AllContext context)
        {
            _context = context;
        }

        // GET: api/AllItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllItems>>> GetTodoItems()
        {
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/AllItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AllItems>> GetAllItems(long id)
        {
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            var allItems = await _context.TodoItems.FindAsync(id);

            if (allItems == null)
            {
                return NotFound();
            }

            return allItems;
        }

        // PUT: api/AllItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAllItems(long id, AllItems allItems)
        {
            if (id != allItems.Id)
            {
                return BadRequest();
            }

            _context.Entry(allItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllItemsExists(id))
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

        // POST: api/AllItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AllItems>> PostAllItems(AllItems allItems)
        {
          if (_context.TodoItems == null)
          {
              return Problem("Entity set 'AllContext.TodoItems'  is null.");
          }

            _context.TodoItems.Add(allItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllItems), new { id = allItems.Id }, allItems);

        }

        // DELETE: api/AllItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAllItems(long id)
        {
            if (_context.TodoItems == null)
            {
                return NotFound();
            }
            var allItems = await _context.TodoItems.FindAsync(id);
            if (allItems == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(allItems);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AllItemsExists(long id)
        {
            return (_context.TodoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
