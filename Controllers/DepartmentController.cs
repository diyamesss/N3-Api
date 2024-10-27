using EntryApi.Database;
using EntryApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DbEntryContext _dbEntryContext;
        public DepartmentController(DbEntryContext dbEntryContext)
        {
            _dbEntryContext = dbEntryContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                return Ok(await _dbEntryContext.Departments.ToListAsync());
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving departments");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(DepartmentDto departmentModel)
        {
            try
            {
                Department department = new Department()
                {
                    DepartmentName = departmentModel.DepartmentName,
                    CreatedBy = departmentModel.CreatedBy,
                    DateCreated = DateTime.UtcNow
                };
                _dbEntryContext.Departments.Add(department);
                await _dbEntryContext.SaveChangesAsync();
                return Ok(department);
            }
            catch (Exception)
            {
                return BadRequest("Error inserting department");
            }
        }
    }
}
