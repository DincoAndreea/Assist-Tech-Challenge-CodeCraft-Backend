
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using TeamFinderDataAccess.Data;
using TeamFinderDataAccess.ProjectDAL;
using TeamFinderModels;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectAccessor _projectAccessor;

        public ProjectController(IProjectAccessor projectAccessor)
        {
            _projectAccessor = projectAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _projectAccessor.GetProjects();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project?>> GetProject(string id)
        {
            return await _projectAccessor.GetProject(id);
        }

        [HttpPost]
        public async Task<ActionResult> Addproject(Project project)
        {
            _projectAccessor.AddProject(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProject(Project project)
        {
            _projectAccessor.UpdateProject(project);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(string id)
        {
            _projectAccessor.DeleteProject(id);
            return Ok();
        }
    }
}

