
using CodeCraft_TeamFinder_Models;
using CodeCraft_TeamFinder_Repository;
using CodeCraft_TeamFinder_Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            User user = await _userService.Get(id);

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

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO login)
        {
            var loginResponse = await _userService.Login(login);

            if (login == null)
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

