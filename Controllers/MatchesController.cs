﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MatchManagementApi.Data;
using MatchManagementApi.Models;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace MatchManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly MatchManagementDataContext _context;

        public MatchesController(MatchManagementDataContext context)
        {
            _context = context;
        }

        // GET: api/Matches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            if (_context.Matches == null)
            {
                return NotFound();
            }
            return await _context.Matches.ToListAsync();
        }

        // GET: api/Matches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            if (_context.Matches == null)
            {
                return NotFound();
            }
            var match = await _context.Matches.FindAsync(id);

            if (match == null)
            {
                return NotFound();
            }

            return match;
        }

        // PUT: api/Matches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(int id, Match match)
        {
            if (match.MatchID.HasValue && id != match.MatchID)
            {
                return BadRequest();
            }
            else if(!match.MatchID.HasValue)
            {
                match.MatchID = id;
            }

            ModelState.ClearValidationState(nameof(match));
            if (!ValidateMatchTime(match.MatchTime))
            {
                return Problem("matchTime must be in format HH:mm");
            }

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        // POST: api/Matches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(Match match)
        {
            if (_context.Matches == null)
            {
                return Problem("Entity set 'MatchOddsDataContext.Matches'  is null.");
            }

            ModelState.ClearValidationState(nameof(match));
            if (!ValidateMatchTime(match.MatchTime))
            {
                return Problem("matchTime must be in format HH:mm");
            }

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatch", new { id = match.MatchID }, match);
        }

        // DELETE: api/Matches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            if (_context.Matches == null)
            {
                return NotFound();
            }
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchExists(int id)
        {
            return (_context.Matches?.Any(e => e.MatchID == id)).GetValueOrDefault();
        }

        private bool ValidateMatchTime(string time)
        {
            System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex("([0-1][\\d]|2[0-4]):[0-5][\\d]");

            return !string.IsNullOrEmpty(time) && rgx.Matches(time).Count == 1;
        }
    }
}
