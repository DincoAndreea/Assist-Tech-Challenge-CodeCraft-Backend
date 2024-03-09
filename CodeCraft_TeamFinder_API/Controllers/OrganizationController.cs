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
    public class OrganizationController : ControllerBase
    {
        private IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<Organization>> GetOrganizations()
        {
            return await _organizationService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganization(string id)
        {
            Organization organization = await _organizationService.Get(id);
            
            if (organization == null)
            {
                return NotFound();
            }

            return Ok(organization);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrganization(Organization organization)
        {
            bool success = await _organizationService.Create(organization);

            if (!success)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetOrganization), new { id = organization.Id }, organization);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrganization(Organization organization)
        {
            bool success = await _organizationService.Update(organization);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrganization(string id)
        {
            bool success = await _organizationService.Delete(id);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
