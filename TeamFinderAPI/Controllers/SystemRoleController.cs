
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using TeamFinderDataAccess.Data;
using TeamFinderDataAccess.SystemRoleDAL;
using TeamFinderModels;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemRoleController : ControllerBase
    {
        private ISystemRoleAccessor _systemroleAccessor;

        public SystemRoleController(ISystemRoleAccessor systemroleAccessor)
        {
            _systemroleAccessor = systemroleAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<SystemRole>> GetSystemRoles()
        {
            return await _systemroleAccessor.GetSystemRoles();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SystemRole?>> GetSystemRole(string id)
        {
            return await _systemroleAccessor.GetSystemRole(id);
        }

        [HttpPost]
        public async Task<ActionResult> Addsystemrole(SystemRole systemrole)
        {
            _systemroleAccessor.AddSystemRole(systemrole);
            return CreatedAtAction(nameof(GetSystemRole), new { id = systemrole.Id }, systemrole);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSystemRole(SystemRole systemrole)
        {
            _systemroleAccessor.UpdateSystemRole(systemrole);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSystemRole(string id)
        {
            _systemroleAccessor.DeleteSystemRole(id);
            return Ok();
        }
    }
}

