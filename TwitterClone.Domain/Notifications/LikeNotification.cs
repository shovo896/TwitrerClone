namespace TwitterClone.Domain.Notifications;

public sealed class LikeNotification : Notification
{
    public LikeNotification(Guid recipientId, Guid actorId, Guid tweetId) : base(recipientId, actorId)
    {
        if (tweetId == Guid.Empty) throw new ArgumentException("Tweet is required.", nameof(tweetId));
        ActorId = actorId;
        TweetId = tweetId;
    }

    public Guid ActorId { get; }
    public Guid TweetId { get; }
    public override string Type => "Like";
}
