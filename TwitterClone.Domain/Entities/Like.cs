namespace TwitterClone.Domain.Entities;

using TwitterClone.Domain.Shared;

public sealed class Like : BaseEntity
{
    public Like(Guid userId, Guid tweetId) : base(userId)
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
