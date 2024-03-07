
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using TeamFinderDataAccess.Data;
using TeamFinderDataAccess.UserDAL;
using TeamFinderModels;

namespace TeamFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserAccessor _userAccessor;

        public UserController(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userAccessor.GetUsers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetUser(string id)
        {
            return await _userAccessor.GetUser(id);
        }

        [HttpPost]
        public async Task<ActionResult> Adduser(User user)
        {
            _userAccessor.AddUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(User user)
        {
            _userAccessor.UpdateUser(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            _userAccessor.DeleteUser(id);
            return Ok();
        }
    }
}

