using EntryApi.Database;
using EntryApi.Enums;
using EntryApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace EntryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        private readonly DbEntryContext _dbEntryContext;
        public EntryController(DbEntryContext dbEntryContext)
        {
            _dbEntryContext = dbEntryContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEntries()
        {
            try
            {
                List<Entry> entries = await _dbEntryContext.Entries.ToListAsync();
                if (!entries.Count.Equals(0))
                    return Ok(entries);
                else
                    return NotFound("No entries found");
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving entries");
            }
        }

        [HttpGet("GetEntryByEntryId/{entryId}")]
        public async Task<IActionResult> GetEntryByEntryId(long entryId)
        {
            try
            {
                var entry = await _dbEntryContext.Entries
                    .Join(_dbEntryContext.Statuses,
                    entry => entry.StatusId,
                    status => status.StatusId,
                    (entry, status) => new { entry, status })
                    .Join(_dbEntryContext.AuditExceptions,
                    entryAndStatus => entryAndStatus.entry.AuditExceptionId,
                    audit => audit.AuditExceptionId,
                    (entryAndStatus, audit) => new { entryAndStatus, audit })
                    .Join(_dbEntryContext.Departments,
                    entryStatusAudit => entryStatusAudit.entryAndStatus.entry.DepartmentId,
                    department => department.DepartmentId,
                    (entryStatusAudit, department) => new { entryStatusAudit, department })
                    .Where(e => e.entryStatusAudit.entryAndStatus.entry.EntryId.Equals(entryId)).SingleOrDefaultAsync();
                if (entry != null)
                    return Ok(entry);
                else
                    return NotFound("No entries found");
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving entries");
            }
        }

        [HttpGet("GetAllEntriesByUserId/{userId}")]
        public async Task<IActionResult> GetAllEntriesByUserId(string userId)
        {
            try
            {
                List<Entry> entries = await _dbEntryContext.Entries.Where(e => e.CreatedBy.ToLower() == userId.ToLower()).ToListAsync();
                if (!entries.Count.Equals(0)) 
                    return Ok(entries);
                else
                    return NotFound("No entries found");
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving entries");
            }
        }

        [HttpGet("SearchEntry/{wildCard}")]
        public async Task<IActionResult> SearchEntry(string wildCard)
        {
            try
            {
                wildCard = wildCard.ToLower();
                var entry = await _dbEntryContext.Entries
                    .Join(_dbEntryContext.Statuses,
                    entry => entry.StatusId,
                    status => status.StatusId,
                    (entry, status) => new { entry, status })
                    .Join(_dbEntryContext.AuditExceptions,
                    entryAndStatus => entryAndStatus.entry.AuditExceptionId,
                    audit => audit.AuditExceptionId,
                    (entryAndStatus, audit) => new { entryAndStatus, audit })
                    .Join(_dbEntryContext.Departments,
                    entryStatusAudit => entryStatusAudit.entryAndStatus.entry.DepartmentId,
                    department => department.DepartmentId,
                    (entryStatusAudit, department) => new {entryStatusAudit, department})
                    .Where(e => e.entryStatusAudit.entryAndStatus.entry.AerNumber.ToLower().Contains(wildCard)
                    || e.department.DepartmentName.ToLower().Contains(wildCard)
                    || e.entryStatusAudit.entryAndStatus.entry.IssuedTo.ToLower().Contains(wildCard)
                    || e.entryStatusAudit.entryAndStatus.status.StatusName.ToLower().Contains(wildCard)
                    || e.entryStatusAudit.audit.AuditExceptionName.ToLower().Contains(wildCard)
                    || e.entryStatusAudit.entryAndStatus.entry.CreatedBy.ToLower().Contains(wildCard))
                    .ToListAsync();

                if (!entry.Count.Equals(0))
                    return Ok(entry);
                else
                    return NotFound("The entry does not exist");
            }
            catch (Exception)
            {
                return BadRequest("Error searching entry");
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertEntry(EntryDto entryModel)
        {
            try
            {
                Entry entry = new Entry()
                {
                    AerNumber = entryModel.AerNumber,
                    DepartmentId = entryModel.DepartmentId,
                    IssuedTo = entryModel.IssuedTo,
                    StatusId = (int)StatusEnum.Created,
                    AuditExceptionId = (int)entryModel.AuditExceptionId,
                    CreatedBy = entryModel.CreatedBy,
                    DateCreated = DateTime.UtcNow
                };
                _dbEntryContext.Entries.Add(entry);
                await _dbEntryContext.SaveChangesAsync();
                return Ok(entry.EntryId);
            }
            catch (Exception)
            {
                return BadRequest("Error posting entry");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEntry(EntryDto entryModel)
        {
            try
            {
                Entry entry = await _dbEntryContext.Entries.Where(e => e.EntryId.Equals(entryModel.EntryId)).SingleOrDefaultAsync();
                if (entry != null)
                {
                    entry.StatusId = (int)entryModel.StatusId;
                    await _dbEntryContext.SaveChangesAsync();
                    return Ok(entry);
                }
                else
                    return NotFound("entry not found");
            }
            catch (Exception)
            {
                return BadRequest("Error updating entry");
            }
        }
    }
}
