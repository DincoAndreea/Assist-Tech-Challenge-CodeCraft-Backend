
using CodeCraft_TeamFinder_Models;
using CodeCraft_TeamFinder_Repository;
using CodeCraft_TeamFinder_Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            var user = await _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            bool success = await _userService.Create(user);

            if (!success)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(User user)
        {
            bool success = await _userService.Update(user);

            if (!success)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            bool success = await _userService.Delete(id);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("Organization/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByOrganization(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            IEnumerable<User> users = await _userService.GetUsersByOrganization(id);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet("Department/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByDepartment(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            IEnumerable<User> users = await _userService.GetUsersByDepartment(id);

            return Ok(users);
        }

        [HttpGet("Project/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByProject(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            IEnumerable<User> users = await _userService.GetUsersByProject(id);

            return Ok(users);
        }

        [HttpGet("Skill/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersBySkill(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            IEnumerable<User> users = await _userService.GetUsersBySkill(id);

            return Ok(users);
        }

        [HttpPost("TeamFinder")]
        public async Task<ActionResult<IEnumerable<TeamFinderResponseDTO>>> TeamFinder(TeamFinderRequestDTO teamFinderRequestDTO)
        {
            if (!ObjectId.TryParse(teamFinderRequestDTO.OrganizationID, out _))
            {
                return BadRequest();
            }

            var users = await _userService.TeamFinder(teamFinderRequestDTO);

            return Ok(users);
        }

        [HttpGet("OrganizationAdmins")]
        public async Task<ActionResult<IEnumerable<User>>> GetOrganizationAdmins(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            IEnumerable<User> users = await _userService.GetOrganizationAdmins(id);

            return Ok(users);
        }

        [HttpPost("DepartmentManagers")]
        public async Task<ActionResult<IEnumerable<User>>> GetDepartmentManagers(DepartmentManagersDTO departmentManagersDTO)
        {
            if (!ObjectId.TryParse(departmentManagersDTO.OrganizationID, out _))
            {
                return BadRequest();
            }

            IEnumerable<User> users = await _userService.GetDepartmentManagers(departmentManagersDTO);

            return Ok(users);
        }

        [HttpGet("ProjectManagers")]
        public async Task<ActionResult<IEnumerable<User>>> GetProjectManagers(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            IEnumerable<User> users = await _userService.GetProjectManagers(id);

            return Ok(users);
        }

        [HttpGet("Employees")]
        public async Task<ActionResult<IEnumerable<User>>> GetEmployees(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            IEnumerable<User> users = await _userService.GetEmployees(id);

            return Ok(users);
        }

        [HttpGet("EmployeesWithoutDepartment")]
        public async Task<ActionResult<IEnumerable<User>>> GetEmployeesWithoutDepartment(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest();
            }

            IEnumerable<User> users = await _userService.GetEmployeesWithoutDepartment(id);

            return Ok(users);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO login)
        {
            var loginResponse = await _userService.Login(login);

            if (loginResponse.Id == null)
            {
                return NotFound();
            }

            return Ok(loginResponse);
        }

        [HttpPost("RegisterAdmin")]
        public async Task<ActionResult> RegisterAdmin(RegisterAdminRequestDTO registerAdminRequest)
        {
            var registered = await _userService.RegisterAdmin(registerAdminRequest);

            if (!registered)
            {
                return BadRequest();
            }

            return Created();
        }

        [HttpPost("RegisterEmployee")]
        public async Task<ActionResult> RegisterEmployee(RegisterEmployeeRequestDTO registerEmployeeRequest)
        {
            var registered = await _userService.RegisterEmployee(registerEmployeeRequest);

            if (!registered)
            {
                return BadRequest();
            }

            return Created();
        }
    }
}

