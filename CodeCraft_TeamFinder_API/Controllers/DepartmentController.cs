
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
    public class DepartmentController : ControllerBase
    {
        private IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _departmentService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            Department department = await _departmentService.Get(id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpGet("Organization/{id}")]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartmentsByOrganization(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            IEnumerable<DepartmentDTO> departments = await _departmentService.GetDepartmentsByOrganization(id);

            return Ok(departments);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDepartment(Department department)
        {
            bool success = await _departmentService.Create(department);

            if (!success)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDepartment(Department department)
        {
            bool success = await _departmentService.Update(department);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(string id)
        {
            bool success = await _departmentService.Delete(id);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

