using TwitterClone.Domain.Entities;

namespace TwitterClone.Domain.Tests.Entities;

public class LikeTests
{
    [Fact]
    public void Constructor_CreatesLikeForAuthenticatedUser()
    {
        var userId = Guid.NewGuid();
        var tweetId = Guid.NewGuid();

        var like = new Like(userId, tweetId);

        Assert.NotEqual(Guid.Empty, like.Id);
        Assert.Equal(userId, like.UserId);
        Assert.Equal(tweetId, like.TweetId);
        Assert.Equal(userId, like.CreatedBy);
    }

    [Fact]
    public void Constructor_RejectsMissingAuthenticatedUser()
    {
        Assert.Throws<ArgumentException>(() => new Like(Guid.Empty, Guid.NewGuid(), Guid.NewGuid()));
    }

    [Fact]
    public void Constructor_RejectsLikeOnBehalfOfAnotherUser()
    {
        Assert.Throws<UnauthorizedAccessException>(() => new Like(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()));
    }
}
