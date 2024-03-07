
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using TeamFinderDataAccess.Data;
using TeamFinderDataAccess.SkillDAL;
using TeamFinderModels;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private ISkillAccessor _skillAccessor;

        public SkillController(ISkillAccessor skillAccessor)
        {
            _skillAccessor = skillAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<Skill>> GetSkills()
        {
            return await _skillAccessor.GetSkills();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Skill?>> GetSkill(string id)
        {
            return await _skillAccessor.GetSkill(id);
        }

        [HttpPost]
        public async Task<ActionResult> Addskill(Skill skill)
        {
            _skillAccessor.AddSkill(skill);
            return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skill);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSkill(Skill skill)
        {
            _skillAccessor.UpdateSkill(skill);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSkill(string id)
        {
            _skillAccessor.DeleteSkill(id);
            return Ok();
        }
    }
}

