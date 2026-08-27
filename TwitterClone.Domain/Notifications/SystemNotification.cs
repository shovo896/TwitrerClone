namespace TwitterClone.Domain.Notifications;

public sealed class SystemNotification : Notification
{
    public SystemNotification(Guid recipientId, string message) : base(recipientId)
    {
        if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Message is required.", nameof(message));
        Message = message.Trim();
    }

    public string Message { get; }
    public override string Type => "System";
    public override string GetMessage() => Message;
}
