using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using TeamFinderDataAccess.Data;
using TeamFinderDataAccess.OrganizationDAL;
using TeamFinderModels;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private IOrganizationAccessor _organizationAccessor;

        public OrganizationController(IOrganizationAccessor organizationAccessor)
        {
            _organizationAccessor = organizationAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<Organization>> GetOrganizations()
        {
            return await _organizationAccessor.GetOrganizations();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Organization?>> GetOrganization(string id)
        {
            return await _organizationAccessor.GetOrganization(id);
        }

        [HttpPost]
        public async Task<ActionResult> Addorganization(Organization organization)
        {
            _organizationAccessor.AddOrganization(organization);
            return CreatedAtAction(nameof(GetOrganization), new { id = organization.Id }, organization);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrganization(Organization organization)
        {
            _organizationAccessor.UpdateOrganization(organization);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrganization(string id)
        {
            _organizationAccessor.DeleteOrganization(id);
            return Ok();
        }
    }
}
