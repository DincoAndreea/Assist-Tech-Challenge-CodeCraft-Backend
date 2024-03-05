
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using TeamFinderDataAccess.Data;
using TeamFinderDataAccess.ProjectRoleDAL;
using TeamFinderModels;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectRoleController : ControllerBase
    {
        private IProjectRoleAccessor _projectroleAccessor;

        public ProjectRoleController(IProjectRoleAccessor projectroleAccessor)
        {
            _projectroleAccessor = projectroleAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectRole>> GetProjectRoles()
        {
            return await _projectroleAccessor.GetProjectRoles();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectRole?>> GetProjectRole(string id)
        {
            return await _projectroleAccessor.GetProjectRole(id);
        }

        [HttpPost]
        public async Task<ActionResult> Addprojectrole(ProjectRole projectrole)
        {
            _projectroleAccessor.AddProjectRole(projectrole);
            return CreatedAtAction(nameof(GetProjectRole), new { id = projectrole.Id }, projectrole);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProjectRole(ProjectRole projectrole)
        {
            _projectroleAccessor.UpdateProjectRole(projectrole);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectRole(string id)
        {
            _projectroleAccessor.DeleteProjectRole(id);
            return Ok();
        }
    }
}

