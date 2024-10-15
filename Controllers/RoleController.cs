using EntryApi.Database;
using EntryApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly DbEntryContext _dbEntryContext;
        public RoleController(DbEntryContext dbEntryContext)
        {
            _dbEntryContext = dbEntryContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                return Ok(await _dbEntryContext.Roles.ToListAsync());
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving roles");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleModel roleModel)
        {
            try
            {
                Role role = new Role()
                {
                    RoleName = roleModel.RoleName,
                    CreatedBy = roleModel.CreatedBy,
                    DateCreated = DateTime.UtcNow
                };
                _dbEntryContext.Roles.Add(role);
                await _dbEntryContext.SaveChangesAsync();
                return Ok(role);
            }
            catch (Exception)
            {
                return BadRequest("Error inserting role");
            }
        }
    }
}
