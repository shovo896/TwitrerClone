using TwitterClone.Domain.Entities;

namespace TwitterClone.Domain.Tests.Entities;

public class TweetTests
{
    [Fact]
    public void Constructor_TrimsContent()
    {
        var tweet = new Tweet(Guid.NewGuid(), "  Hello Twitter  ");

        Assert.Equal("Hello Twitter", tweet.Content);
        Assert.NotEqual(Guid.Empty, tweet.Id);
    }

    [Fact]
    public void Constructor_RejectsEmptyAuthor()
    {
        Assert.Throws<ArgumentException>(() => new Tweet(Guid.Empty, "Hello Twitter"));
    }

    [Fact]
    public void SetContent_RejectsContentOver280Characters()
    {
        var tweet = new Tweet(Guid.NewGuid(), "Hello Twitter");

        Assert.Throws<ArgumentException>(() => tweet.SetContent(new string('a', 281)));
    }
}
