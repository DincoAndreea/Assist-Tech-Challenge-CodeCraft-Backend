
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using TeamFinderDataAccess.Data;
using TeamFinderDataAccess.DepartmentDAL;
using TeamFinderModels;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IDepartmentAccessor _departmentAccessor;

        public DepartmentController(IDepartmentAccessor departmentAccessor)
        {
            _departmentAccessor = departmentAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _departmentAccessor.GetDepartments();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department?>> GetDepartment(string id)
        {
            return await _departmentAccessor.GetDepartment(id);
        }

        [HttpPost]
        public async Task<ActionResult> Adddepartment(Department department)
        {
            _departmentAccessor.AddDepartment(department);
            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDepartment(Department department)
        {
            _departmentAccessor.UpdateDepartment(department);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(string id)
        {
            _departmentAccessor.DeleteDepartment(id);
            return Ok();
        }
    }
}

