namespace TwitterClone.Domain.Notifications;

public sealed class CommentNotification : Notification
{
    public CommentNotification(Guid recipientId, Guid actorId, Guid tweetId, string comment) : base(recipientId, actorId)
    {
        if (actorId == Guid.Empty) throw new ArgumentException("Actor is required.", nameof(actorId));
        if (tweetId == Guid.Empty) throw new ArgumentException("Tweet is required.", nameof(tweetId));
        if (string.IsNullOrWhiteSpace(comment)) throw new ArgumentException("Comment is required.", nameof(comment));
        ActorId = actorId;
        TweetId = tweetId;
        Comment = comment.Trim();
    }

    public Guid ActorId { get; }
    public Guid TweetId { get; }
    public string Comment { get; }
    public override string Type => "Comment";
    public override string GetMessage() => $"User with ID {ActorId} commented on your tweet: {Comment}";
}
