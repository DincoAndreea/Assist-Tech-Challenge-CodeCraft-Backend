
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using TeamFinderDataAccess.Data;
using TeamFinderDataAccess.SkillCategoryDAL;
using TeamFinderModels;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillCategoryController : ControllerBase
    {
        private ISkillCategoryAccessor _skillCategoryAccessor;

        public SkillCategoryController(ISkillCategoryAccessor skillCategoryAccessor)
        {
            _skillCategoryAccessor = skillCategoryAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<SkillCategory>> GetSkillCategorys()
        {
            return await _skillCategoryAccessor.GetSkillCategorys();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkillCategory?>> GetSkillCategory(string id)
        {
            return await _skillCategoryAccessor.GetSkillCategory(id);
        }

        [HttpPost]
        public async Task<ActionResult> AddskillCategory(SkillCategory skillCategory)
        {
            _skillCategoryAccessor.AddSkillCategory(skillCategory);
            return CreatedAtAction(nameof(GetSkillCategory), new { id = skillCategory.Id }, skillCategory);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSkillCategory(SkillCategory skillCategory)
        {
            _skillCategoryAccessor.UpdateSkillCategory(skillCategory);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSkillCategory(string id)
        {
            _skillCategoryAccessor.DeleteSkillCategory(id);
            return Ok();
        }
    }
}

