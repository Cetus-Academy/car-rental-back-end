using NetCoreTemplate.Domain.Common.Enums;

namespace NetCoreTemplate.Domain.Common.Entities;

public abstract class AuditableEntity
{
    public string CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
        
    public string LastModifiedBy { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; }
        
    public EntryStatus EntryStatus { get; set; }
        
    public string InactivatedBy { get; set; }
    public DateTimeOffset? InactivatedAt { get; set; }
}