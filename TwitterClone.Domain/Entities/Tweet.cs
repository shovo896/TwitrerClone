namespace TwitterClone.Domain.Entities;

public class Tweet
{
    private readonly Guid _id;
    private readonly Guid _authorId;
    private string _content = string.Empty;

    public Tweet(Guid authorId, string content)
    {
        if (authorId == Guid.Empty)
            throw new ArgumentException("Author is required.", nameof(authorId));

        _id = Guid.NewGuid();
        _authorId = authorId;
        SetContent(content);
    }

    public Guid Id => _id;
    public Guid AuthorId => _authorId;
    public string Content => _content;

    public void SetContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Tweet cannot be empty.", nameof(content));

        if (content.Length > 280)
            throw new ArgumentException("Tweet cannot exceed 280 characters.", nameof(content));

        _content = content.Trim();
    }
}
