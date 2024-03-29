
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
    public class TeamRoleController : ControllerBase
    {
        private ITeamRoleService _teamRoleService;

        public TeamRoleController(ITeamRoleService teamRoleService)
        {
            _teamRoleService = teamRoleService;
        }

        [HttpGet]
        public async Task<IEnumerable<TeamRole>> GetTeamRoles()
        {
            return await _teamRoleService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamRole>> GetTeamRole(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            TeamRole teamRole = await _teamRoleService.Get(id);

            if (teamRole == null)
            {
                return NotFound();
            }

            return Ok(teamRole);
        }

        [HttpGet("Organization/{id}")]
        public async Task<ActionResult<TeamRole>> GetTeamRolesByOrganization(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            var teamRole = await _teamRoleService.GetTeamRolesByOrganization(id);

            return Ok(teamRole);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTeamRole(TeamRole teamRole)
        {
            bool success = await _teamRoleService.Create(teamRole);

            if (!success)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetTeamRole), new { id = teamRole.Id }, teamRole);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTeamRole(TeamRole teamRole)
        {
            bool success = await _teamRoleService.Update(teamRole);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeamRole(string id)
        {
            bool success = await _teamRoleService.Delete(id);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

