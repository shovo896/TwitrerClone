using TwitterClone.Domain.Entities;

namespace TwitterClone.Domain.Tests.Entities;

public class UserTests
{
    [Fact]
    public void Constructor_TrimsUsernameAndEmail()
    {
        var user = new User("  shuvo  ", "  shuvo@example.com  ");

        Assert.Equal("shuvo", user.Username);
        Assert.Equal("shuvo@example.com", user.Email);
        Assert.NotEqual(Guid.Empty, user.Id);
    }

    [Theory]
    [InlineData("", "user@example.com")]
    [InlineData("user", "")]
    public void Constructor_RejectsMissingRequiredValues(string username, string email)
    {
        Assert.Throws<ArgumentException>(() => new User(username, email));
    }
}
