namespace TwitterClone.Domain.Notifications;

public sealed class LikeNotification : Notification
{
    public LikeNotification(Guid recipientId, Guid actorId, Guid tweetId) : base(recipientId, actorId)
    {
        if (actorId == Guid.Empty) throw new ArgumentException("Actor is required.", nameof(actorId));
        if (tweetId == Guid.Empty) throw new ArgumentException("Tweet is required.", nameof(tweetId));
        ActorId = actorId;
        TweetId = tweetId;
    }

    public Guid ActorId { get; }
    public Guid TweetId { get; }
    public override string Type => "Like";
    public override string GetMessage() => $"User with ID {ActorId} liked your tweet.";
}
