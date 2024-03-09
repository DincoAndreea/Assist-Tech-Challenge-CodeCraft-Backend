
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
    public class SkillCategoryController : ControllerBase
    {
        private ISkillCategoryService _skillCategoryService;

        public SkillCategoryController(ISkillCategoryService skillCategoryService)
        {
            _skillCategoryService = skillCategoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<SkillCategory>> GetSkillCategorys()
        {
            return await _skillCategoryService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkillCategory>> GetSkillCategory(string id)
        {
            SkillCategory skillCategory = await _skillCategoryService.Get(id);

            if (skillCategory == null)
            {
                return NotFound();
            }

            return Ok(skillCategory);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSkillCategory(SkillCategory skillCategory)
        {
            bool success = await _skillCategoryService.Create(skillCategory);

            if (!success)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetSkillCategory), new { id = skillCategory.Id }, skillCategory);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSkillCategory(SkillCategory skillCategory)
        {
            bool success = await _skillCategoryService.Update(skillCategory);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSkillCategory(string id)
        {
            bool success = await _skillCategoryService.Delete(id);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

