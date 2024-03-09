
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
    public class SystemRoleController : ControllerBase
    {
        private ISystemRoleService _systemRoleService;

        public SystemRoleController(ISystemRoleService systemRoleService)
        {
            _systemRoleService = systemRoleService;
        }

        [HttpGet]
        public async Task<IEnumerable<SystemRole>> GetSystemRoles()
        {
            return await _systemRoleService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SystemRole>> GetSystemRole(string id)
        {
            SystemRole systemRole = await _systemRoleService.Get(id);

            if (systemRole == null)
            {
                return NotFound();
            }

            return Ok(systemRole);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSystemRole(SystemRole systemRole)
        {
            bool success = await _systemRoleService.Create(systemRole);

            if (!success)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetSystemRole), new { id = systemRole.Id }, systemRole);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSystemRole(SystemRole systemRole)
        {
            bool success = await _systemRoleService.Update(systemRole);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSystemRole(string id)
        {
            bool success = await _systemRoleService.Delete(id);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

