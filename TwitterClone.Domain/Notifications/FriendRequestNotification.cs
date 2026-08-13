namespace TwitterClone.Domain.Notifications;

public sealed class FriendRequestNotification : Notification
{
    public FriendRequestNotification(Guid recipientId, Guid requesterId) : base(recipientId, requesterId)
    {
        RequesterId = requesterId;
    }

    public Guid RequesterId { get; }
    public override string Type => "FriendRequest";
}
