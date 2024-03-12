
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
    public class ProjectTeamController : ControllerBase
    {
        private IProjectTeamService _projectTeamService;

        public ProjectTeamController(IProjectTeamService projectTeamService)
        {
            _projectTeamService = projectTeamService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectTeam>> GetProjectTeams()
        {
            return await _projectTeamService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTeam>> GetProjectTeam(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            ProjectTeam projectTeam = await _projectTeamService.Get(id);

            if (projectTeam == null)
            {
                return NotFound();
            }

            return Ok(projectTeam);
        }

        [HttpGet("Project/{id}")]
        public async Task<ActionResult<IEnumerable<ProjectTeam>>> GetProjectTeamByProject(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            var projectTeam = await _projectTeamService.GetProjectTeamByProject(id);

            return Ok(projectTeam);
        }

        [HttpGet("Project/{id}/TeamMembers")]
        public async Task<ActionResult<ProjectTeamMembersDTO>> GetProjectTeamMembers(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            var projectTeamMembers = await _projectTeamService.GetProjectTeamMembers(id);

            return Ok(projectTeamMembers);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProjectTeam(ProjectTeam projectTeam)
        {
            bool success = await _projectTeamService.Create(projectTeam);

            if (!success)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetProjectTeam), new { id = projectTeam.Id }, projectTeam);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProjectTeam(ProjectTeam projectTeam)
        {
            bool success = await _projectTeamService.Update(projectTeam);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectTeam(string id)
        {
            bool success = await _projectTeamService.Delete(id);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

