using System;
using System.Collections.Generic;

namespace EntryApi.Database;

public partial class Entry
{
    public long EntryId { get; set; }

    public string AerNumber { get; set; } = null!;

    public long DepartmentId { get; set; }

    public string IssuedTo { get; set; } = null!;

    public long StatusId { get; set; }

    public long AuditExceptionId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool IsDeleted { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DateDeleted { get; set; }
}
