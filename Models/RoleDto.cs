﻿namespace EntryApi.Models
{
    public class RoleDto
    {
        public long RoleId { get; set; }

        public string RoleName { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }

        public bool IsDeleted { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? DateDeleted { get; set; }
    }
}
