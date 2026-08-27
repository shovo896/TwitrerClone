namespace TwitterClone.Domain.Shared;

public abstract class BaseEntity
{
    protected BaseEntity(Guid? createdBy = null)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
    }

    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; private set; }
    public Guid? CreatedBy { get; }
    public Guid? ModifiedBy { get; private set; }

    protected void MarkModified(Guid modifiedBy)
    {
        ModifiedAt = DateTime.UtcNow;
        ModifiedBy = modifiedBy;
    }

    public virtual string Describe() => $"Entity {Id}";
}
