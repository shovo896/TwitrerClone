namespace TwitterClone.Domain.Entities;

using TwitterClone.Domain.Shared;

public sealed class Like : BaseEntity
{
    public Like(Guid userId, Guid tweetId)
        : this(userId, userId, tweetId)
    {
    }

    public Like(Guid authenticatedUserId, Guid userId, Guid tweetId) : base(authenticatedUserId)
    {
        if (authenticatedUserId == Guid.Empty) throw new ArgumentException("Authenticated user is required.", nameof(authenticatedUserId));
        if (userId == Guid.Empty) throw new ArgumentException("User is required.", nameof(userId));
        if (tweetId == Guid.Empty) throw new ArgumentException("Tweet is required.", nameof(tweetId));
        if (authenticatedUserId != userId) throw new UnauthorizedAccessException("Authenticated user cannot like on behalf of another user.");

        UserId = userId;
        TweetId = tweetId;
        LikedAt = DateTime.UtcNow;
    }

    public Guid UserId { get; }
    public Guid TweetId { get; }
    public DateTime LikedAt { get; }
}
