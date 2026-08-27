namespace TwitterClone.Domain.Entities;

using TwitterClone.Domain.Shared;

public class Retweet : BaseEntity
{
    private readonly Guid _userId;
    private readonly Guid _tweetId;
    private readonly DateTime _retweetedAt;

    public Retweet(Guid userId, Guid tweetId) : base(userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User is required.", nameof(userId));

        if (tweetId == Guid.Empty)
            throw new ArgumentException("Tweet is required.", nameof(tweetId));

        _userId = userId;
        _tweetId = tweetId;
        _retweetedAt = DateTime.UtcNow;
    }

    public Guid UserId => _userId;
    public Guid TweetId => _tweetId;
    public DateTime RetweetedAt => _retweetedAt;
}
