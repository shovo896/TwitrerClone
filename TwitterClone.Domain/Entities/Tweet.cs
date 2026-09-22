namespace TwitterClone.Domain.Entities;

using TwitterClone.Domain.Shared;

public class Tweet : BaseEntity, ILikeable
{
    private readonly Guid _authorId;
    private string _content = string.Empty;

    public Tweet(Guid authorId, string content) : base(authorId)
    {
        if (authorId == Guid.Empty)
            throw new ArgumentException("Author is required.", nameof(authorId));

        _authorId = authorId;
        SetContent(content);
    }

    public Guid AuthorId => _authorId;
    public string Content => _content;
    public bool CanBeLiked() => !string.IsNullOrWhiteSpace(Content);

    public void SetContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Tweet cannot be empty.", nameof(content));

        if (content.Length > 280)
            throw new ArgumentException("Tweet cannot exceed 280 characters.", nameof(content));

        _content = content.Trim();
    }

    public override string Describe() => $"Tweet: {Content}";
}
