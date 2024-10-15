using System;
using System.Collections.Generic;

namespace EntryApi.Database;

public partial class Status
{
    public long StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public string? StatusDescription { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool IsDeleted { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DateDeleted { get; set; }
}
