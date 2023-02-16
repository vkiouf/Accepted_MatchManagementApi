using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MatchManagementApi.Data;
using MatchManagementApi.Models;
using Microsoft.AspNetCore.Razor.Language;

namespace MatchManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchOddsController : ControllerBase
    {
        private readonly MatchManagementDataContext _context;

        public MatchOddsController(MatchManagementDataContext context)
        {
            _context = context;
        }

        // GET: api/MatchOdds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchOdds>>> GetMatchOddss()
        {
            if (_context.MatchOddss == null)
            {
                return NotFound();
            }
            return await _context.MatchOddss.ToListAsync();
        }

        // GET: api/MatchOdds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchOdds>> GetMatchOdds(int id)
        {
            if (_context.MatchOddss == null)
            {
                return NotFound();
            }
            var matchOdds = await _context.MatchOddss.FindAsync(id);

            if (matchOdds == null)
            {
                return NotFound();
            }

            return matchOdds;
        }

        // PUT: api/MatchOdds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatchOdds(int id, MatchOdds matchOdds)
        {
            if (matchOdds.MatchOddsID.HasValue && id != matchOdds.MatchOddsID)
            {
                return BadRequest();
            }
            else if(!matchOdds.MatchOddsID.HasValue)
            {
                matchOdds.MatchOddsID = id;
            }


            _context.Entry(matchOdds).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchOddsExists(id))
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

        // POST: api/MatchOdds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MatchOdds>> PostMatchOdds(MatchOdds matchOdds)
        {
            if (_context.MatchOddss == null)
            {
                return Problem("Entity set 'MatchOddsDataContext.MatchOddss'  is null.");
            }

            if (!_context.Matches.Any(mat => mat.MatchID == matchOdds.MatchId))
            {
                return Problem(string.Format("Match Id {0} does not exist", matchOdds.MatchId));
            }

            _context.MatchOddss.Add(matchOdds);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatchOdds", new { id = matchOdds.MatchOddsID }, matchOdds);
        }

        // DELETE: api/MatchOdds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatchOdds(int id)
        {
            if (_context.MatchOddss == null)
            {
                return NotFound();
            }
            var matchOdds = await _context.MatchOddss.FindAsync(id);
            if (matchOdds == null)
            {
                return NotFound();
            }

            _context.MatchOddss.Remove(matchOdds);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchOddsExists(int id)
        {
            return (_context.MatchOddss?.Any(e => e.MatchOddsID == id)).GetValueOrDefault();
        }
    }
}
