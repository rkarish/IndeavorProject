using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Indeavor.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;

namespace Indeavor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly SkillContext _context;

        public SkillsController(SkillContext context)
        {
            _context = context;
        }

        // GET: api/Skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            return await _context.Skills.ToListAsync();
        }

        // GET: api/Skills/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkill(long id)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            return skill;
        }

        // PUT: api/Skills/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(long id, Skill skill)
        {
            if (id != skill.Id)
            {
                return BadRequest();
            }

            _context.Entry(skill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
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

        // PATCH: api/Skills/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchSkill(long id, [FromBody] JsonPatchDocument<Skill> patchSkill)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            if (patchSkill != null)
            {
                patchSkill.ApplyTo(skill, ModelState);
            }

            return Ok(skill);
        }

        // POST: api/Skills
        [HttpPost]
        public async Task<ActionResult<Skill>> PostSkill(Skill skill)
        {
            _context.Skills.Add(skill);
            skill.CreationTime = DateTime.Now;

            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skill);
        }

        // DELETE: api/Skills/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(long id)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SkillExists(long id)
        {
            return _context.Skills.Any(e => e.Id == id);
        }
    }
}
