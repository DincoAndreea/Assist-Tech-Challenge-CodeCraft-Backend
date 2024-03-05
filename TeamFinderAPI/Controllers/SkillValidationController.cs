
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using TeamFinderDataAccess.Data;
using TeamFinderDataAccess.SkillValidationDAL;
using TeamFinderModels;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillValidationController : ControllerBase
    {
        private ISkillValidationAccessor _skillvalidationAccessor;

        public SkillValidationController(ISkillValidationAccessor skillvalidationAccessor)
        {
            _skillvalidationAccessor = skillvalidationAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<SkillValidation>> GetSkillValidations()
        {
            return await _skillvalidationAccessor.GetSkillValidations();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkillValidation?>> GetSkillValidation(string id)
        {
            return await _skillvalidationAccessor.GetSkillValidation(id);
        }

        [HttpPost]
        public async Task<ActionResult> Addskillvalidation(SkillValidation skillvalidation)
        {
            _skillvalidationAccessor.AddSkillValidation(skillvalidation);
            return CreatedAtAction(nameof(GetSkillValidation), new { id = skillvalidation.Id }, skillvalidation);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSkillValidation(SkillValidation skillvalidation)
        {
            _skillvalidationAccessor.UpdateSkillValidation(skillvalidation);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSkillValidation(string id)
        {
            _skillvalidationAccessor.DeleteSkillValidation(id);
            return Ok();
        }
    }
}

