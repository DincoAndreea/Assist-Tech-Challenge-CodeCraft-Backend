
using CodeCraft_TeamFinder_Models;
using CodeCraft_TeamFinder_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using CodeCraft_TeamFinder_Services.Interfaces;
using MongoDB.Bson;

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
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            Skill skill = await _skillService.Get(id);

            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        [HttpGet("Organization/{id}")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkillsByorganization(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            var skills = await _skillService.GetSkillsByOrganization(id);

            return Ok(skills);
        }

        [HttpGet("SkillCategory/{id}")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkillsBySkillCategory(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            var skills = await _skillService.GetSkillsBySkillCategory(id);

            return Ok(skills);
        }
        [HttpGet("Department/{id}")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkillsByDepartment(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            var skills = await _skillService.GetSkillsByDepartment(id);

            if (skills == null)
            {
                return NotFound();
            }

            return Ok(skills);
        }

        [HttpGet("Author/{id}")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkillsByAuthor(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            var skills = await _skillService.GetSkillsByAuthor(id);

            return Ok(skills);
        }

        [HttpPost("SkillStatistics")]
        public async Task<ActionResult<SkillStatisticsResponseDTO>> GetSkillStatistics(SkillStatisticsRequestDTO skillStatisticsRequestDTO)
        {
            if (!ObjectId.TryParse(skillStatisticsRequestDTO.DepartmentID, out _) || !ObjectId.TryParse(skillStatisticsRequestDTO.SkillID, out _))
            {
                return BadRequest();
            }

            var skillStatistics = await _skillService.GetSkillStatistics(skillStatisticsRequestDTO);

            return Ok(skillStatistics);
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

