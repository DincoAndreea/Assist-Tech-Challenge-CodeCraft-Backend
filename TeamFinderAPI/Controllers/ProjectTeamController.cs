
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using TeamFinderDataAccess.Data;
using TeamFinderDataAccess.ProjectTeamDAL;
using TeamFinderModels;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTeamController : ControllerBase
    {
        private IProjectTeamAccessor _projectTeamAccessor;

        public ProjectTeamController(IProjectTeamAccessor projectTeamAccessor)
        {
            _projectTeamAccessor = projectTeamAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectTeam>> GetProjectTeams()
        {
            return await _projectTeamAccessor.GetProjectTeams();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTeam?>> GetProjectTeam(string id)
        {
            return await _projectTeamAccessor.GetProjectTeam(id);
        }

        [HttpPost]
        public async Task<ActionResult> AddprojectTeam(ProjectTeam projectTeam)
        {
            _projectTeamAccessor.AddProjectTeam(projectTeam);
            return CreatedAtAction(nameof(GetProjectTeam), new { id = projectTeam.Id }, projectTeam);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProjectTeam(ProjectTeam projectTeam)
        {
            _projectTeamAccessor.UpdateProjectTeam(projectTeam);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectTeam(string id)
        {
            _projectTeamAccessor.DeleteProjectTeam(id);
            return Ok();
        }
    }
}

