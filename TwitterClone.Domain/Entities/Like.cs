namespace TwitterClone.Domain.Entities;

public class Like
{
    public Like(Guid userId, Guid tweetId)
        : this(userId, userId, tweetId)
    {
    }

    public Like(Guid authenticatedUserId, Guid userId, Guid tweetId)
    {
        if (authenticatedUserId == Guid.Empty) throw new ArgumentException("Authenticated user is required.", nameof(authenticatedUserId));
        if (userId == Guid.Empty) throw new ArgumentException("User is required.", nameof(userId));
        if (tweetId == Guid.Empty) throw new ArgumentException("Tweet is required.", nameof(tweetId));
        if (authenticatedUserId != userId) throw new UnauthorizedAccessException("Authenticated user cannot like on behalf of another user.");

        Id = Guid.NewGuid();
        UserId = userId;
        TweetId = tweetId;
        LikedAt = DateTime.UtcNow;
        CreatedBy = authenticatedUserId;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public Guid TweetId { get; }
    public DateTime LikedAt { get; }
    public Guid CreatedBy { get; }
}
