namespace Core.Domain.Common;

public interface IAuditedEntity
{
    public DateTime CreatedOn { get; set; }
}
