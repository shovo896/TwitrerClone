namespace TwitterClone.Domain.Entities;

public class Retweet
{
    private readonly Guid _id;
    private readonly Guid _userId;
    private readonly Guid _tweetId;
    private readonly DateTime _retweetedAt;

    public Retweet(Guid userId, Guid tweetId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User is required.", nameof(userId));

        if (tweetId == Guid.Empty)
            throw new ArgumentException("Tweet is required.", nameof(tweetId));

        _id = Guid.NewGuid();
        _userId = userId;
        _tweetId = tweetId;
        _retweetedAt = DateTime.UtcNow;
    }

    public Guid Id => _id;
    public Guid UserId => _userId;
    public Guid TweetId => _tweetId;
    public DateTime RetweetedAt => _retweetedAt;
}
