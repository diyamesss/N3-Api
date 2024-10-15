using System;
using System.Collections.Generic;

namespace EntryApi.Database;

public partial class Department
{
    public long DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool IsDeleted { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DateDeleted { get; set; }
}
