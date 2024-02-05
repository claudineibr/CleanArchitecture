namespace CleanArchitecture.Domain.Common;
public class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset DateCreated { get; set; }
    public string CreatedBy { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }
    public string DeletedBy { get; set; }
}
