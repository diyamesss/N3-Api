namespace EntryApi.Models;

public partial class UserDto
{
    public long UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? DepartmentId { get; set; }

    public long RoleId { get; set; }

    public string? LastLogin { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool IsDeleted { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DateDeleted { get; set; }
}