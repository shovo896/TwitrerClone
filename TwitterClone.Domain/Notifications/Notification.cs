namespace TwitterClone.Domain.Notifications;

using TwitterClone.Domain.Shared;

public abstract class Notification : BaseEntity
{
    protected Notification(Guid recipientId, Guid? createdBy = null) : base(createdBy)
    {
        if (recipientId == Guid.Empty) throw new ArgumentException("Recipient is required.", nameof(recipientId));
        RecipientId = recipientId;
    }

    public Guid RecipientId { get; }
    public abstract string Type { get; }
    public string GetNotificationInfo() => $"RecipientId: {RecipientId}, Notification Type: {Type}";
    public override string Describe() => $"{Type} notification for {RecipientId}";
    public abstract string GetMessage();
}
