using TwitterClone.Domain.Shared;

namespace TwitterClone.Domain.Entities;

public sealed class Bookmark : BaseEntity
{
    public Bookmark(Guid userId, Guid tweetId) : base(userId)
    {
        if (userId == Guid.Empty) throw new ArgumentException("User is required.", nameof(userId));
        if (tweetId == Guid.Empty) throw new ArgumentException("Tweet is required.", nameof(tweetId));

        UserId = userId;
        TweetId = tweetId;
    }

    public Guid UserId { get; }
    public Guid TweetId { get; }

    public override string Describe() => $"{base.Describe()} bookmarked tweet {TweetId} for user {UserId}";
}
