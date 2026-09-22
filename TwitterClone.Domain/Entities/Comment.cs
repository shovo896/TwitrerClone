namespace TwitterClone.Domain.Entities;

using TwitterClone.Domain.Shared;

public class Comment : BaseEntity, ILikeable
{
    private string _content = string.Empty;

    public Comment(Guid authorId, Guid tweetId, string content) : base(authorId)
    {
        if (authorId == Guid.Empty) throw new ArgumentException("Author is required.", nameof(authorId));
        if (tweetId == Guid.Empty) throw new ArgumentException("Tweet is required.", nameof(tweetId));

        AuthorId = authorId;
        TweetId = tweetId;
        SetContent(content);
    }

    public Guid AuthorId { get; }
    public Guid TweetId { get; }
    public string Content => _content;

    public void SetContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Comment cannot be empty.", nameof(content));

        if (content.Length > 140)
            throw new ArgumentException("Comment cannot exceed 140 characters.", nameof(content));

        _content = content.Trim();
    }

    public bool CanBeLiked() => !string.IsNullOrWhiteSpace(Content);
    public override string Describe() => $"Comment: {Content}";
}
