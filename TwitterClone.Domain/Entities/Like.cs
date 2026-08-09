namespace TwitterClone.Domain.Entities;

public class Like
{
    public Like(Guid userId, Guid tweetId)
    {
        if (userId == Guid.Empty) throw new ArgumentException("User is required.", nameof(userId));
        if (tweetId == Guid.Empty) throw new ArgumentException("Tweet is required.", nameof(tweetId));

        UserId = userId;
        TweetId = tweetId;
        LikedAt = DateTime.UtcNow;
    }

    public Guid UserId { get; }
    public Guid TweetId { get; }
    public DateTime LikedAt { get; }
}
