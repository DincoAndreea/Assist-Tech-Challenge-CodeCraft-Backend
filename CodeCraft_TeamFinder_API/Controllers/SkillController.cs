
using CodeCraft_TeamFinder_Models;
using CodeCraft_TeamFinder_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using CodeCraft_TeamFinder_Services.Interfaces;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<IEnumerable<Skill>> GetSkills()
        {
            return await _skillService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkill(string id)
        {
            Skill skill = await _skillService.Get(id);

            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSkill(Skill skill)
        {
            bool success = await _skillService.Create(skill);

            if (!success)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skill);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSkill(Skill skill)
        {
            bool success = await _skillService.Update(skill);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSkill(string id)
        {
            bool success = await _skillService.Delete(id);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

