
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using TeamFinderDataAccess.Data;
using TeamFinderDataAccess.TeamRoleDAL;
using TeamFinderModels;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamRoleController : ControllerBase
    {
        private ITeamRoleAccessor _teamroleAccessor;

        public TeamRoleController(ITeamRoleAccessor teamroleAccessor)
        {
            _teamroleAccessor = teamroleAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<TeamRole>> GetTeamRoles()
        {
            return await _teamroleAccessor.GetTeamRoles();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamRole?>> GetTeamRole(string id)
        {
            return await _teamroleAccessor.GetTeamRole(id);
        }

        [HttpPost]
        public async Task<ActionResult> Addteamrole(TeamRole teamrole)
        {
            _teamroleAccessor.AddTeamRole(teamrole);
            return CreatedAtAction(nameof(GetTeamRole), new { id = teamrole.Id }, teamrole);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTeamRole(TeamRole teamrole)
        {
            _teamroleAccessor.UpdateTeamRole(teamrole);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeamRole(string id)
        {
            _teamroleAccessor.DeleteTeamRole(id);
            return Ok();
        }
    }
}

