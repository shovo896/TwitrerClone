namespace TwitterClone.Domain.Notifications;

public sealed class FriendRequestNotification : Notification
{
    public FriendRequestNotification(Guid recipientId, Guid requesterId) : base(recipientId, requesterId)
    {
        if (requesterId == Guid.Empty) throw new ArgumentException("Requester is required.", nameof(requesterId));
        RequesterId = requesterId;
    }

    public Guid RequesterId { get; }
    public override string Type => "FriendRequest";
    public override string GetMessage() => $"User with ID {RequesterId} sent you a friend request.";
}
