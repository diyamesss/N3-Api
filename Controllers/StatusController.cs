using EntryApi.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntryApi.Models;

namespace EntryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly DbEntryContext _dbEntryContext;
        public StatusController(DbEntryContext dbEntryContext)
        {
            _dbEntryContext = dbEntryContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatuses()
        {
            try
            {
                return Ok(await _dbEntryContext.Statuses.ToListAsync());
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving statuses");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddStatus(StatusModel statusModel)
        {
            try
            {
                Status status = new Status()
                {
                    StatusName = statusModel.StatusName,
                    StatusDescription = statusModel.StatusDescription,
                    CreatedBy = statusModel.CreatedBy,
                    DateCreated = DateTime.UtcNow
                };
                _dbEntryContext.Statuses.Add(status);
                await _dbEntryContext.SaveChangesAsync();
                return Ok(status);
            }
            catch (Exception)
            {
                return BadRequest("Error inserting status");
            }
        }
    }
}
