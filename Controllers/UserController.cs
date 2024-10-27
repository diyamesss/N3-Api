using EntryApi.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntryApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using EntryApi.Services.Interface;
using EntryApi.Services.Implementation;

namespace EntryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DbEntryContext _dbEntryContext;
        private readonly IUserService _userService;

        public UserController(DbEntryContext dbEntryContext, IUserService userService)
        {
            _dbEntryContext = dbEntryContext;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                User user = await _dbEntryContext.Users
    .Where(u => u.Username == username && u.Password == password)
    .SingleAsync();
                if (user != null)
                {
                    user.LastLogin = DateTime.UtcNow.ToString();
                    await _dbEntryContext.SaveChangesAsync();
                    return Ok(user);
                }
                else
                    return BadRequest("Invalid username or password");
            }
            catch (Exception)
            {
                return BadRequest("Error logging in");
            }

        }

        [HttpPost]
        public async Task<IActionResult> InsertUser(UserDto userModel)
        {
            try
            {
                User user = new User()
                {
                    Username = userModel.Username,
                    Password = userModel.Password,
                    FirstName = userModel.FirstName,
                    MiddleName = userModel.MiddleName,
                    LastName = userModel.LastName,
                    RoleId = userModel.RoleId,
                    CreatedBy = userModel.CreatedBy,
                    DateCreated = DateTime.Now
                };
                return Ok(await _userService.InsertUser(user));
            }
            catch (Exception)
            {
                return BadRequest("Error inserting user");
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto userModel)
        {
            try
            {
                User user = await _userService.GetUserById(userModel.UserId);
                if (user != null)
                {
                    user.Password = userModel.Password;
                    user.FirstName = userModel.FirstName;
                    user.MiddleName = userModel.MiddleName;
                    user.LastName = userModel.LastName;
                    user.UpdatedBy = userModel.UpdatedBy;
                    user.DateUpdated = DateTime.UtcNow;
                    return Ok(await _userService.UpdateUser(user));
                }
                else
                    return NotFound("User not found");
            }
            catch (Exception)
            {
                return BadRequest("Error updating user");
            }
        }

        [HttpPatch]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                User user = await _userService.GetUserById(userId);
                if (user != null)
                {
                    user.IsDeleted = true;
                    await _dbEntryContext.SaveChangesAsync();
                    return Ok(user);
                }
                else
                    return NotFound("User not found");
            }
            catch (Exception)
            {
                return BadRequest("Error deleting user");
            }
        }
    }
}
