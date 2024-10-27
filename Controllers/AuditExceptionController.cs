using EntryApi.Database;
using EntryApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditExceptionController : ControllerBase
    {
        private readonly DbEntryContext _dbEntryContext;
        public AuditExceptionController(DbEntryContext dbEntryContext)
        {
            _dbEntryContext = dbEntryContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuditExceptions()
        {
            try
            {
                return Ok(await _dbEntryContext.AuditExceptions.ToListAsync());
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving audit exceptions");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAuditException(AuditExceptionDto auditExceptionModel)
        {
            try
            {
                AuditException auditException = new AuditException()
                {
                    AuditExceptionName = auditExceptionModel.AuditExceptionName,
                    CreatedBy = auditExceptionModel.CreatedBy,
                    DateCreated = DateTime.UtcNow
                };
                _dbEntryContext.AuditExceptions.Add(auditException);
                await _dbEntryContext.SaveChangesAsync();
                return Ok(auditException);
            }
            catch (Exception)
            {
                return BadRequest("Error inserting audit exception");
            }
        }
    }
}
