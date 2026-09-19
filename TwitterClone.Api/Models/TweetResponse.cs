namespace TwitterClone.Api.Models;

public sealed record TweetResponse(
    int Id,
    string Text,
    string Author,
    string Handle,
    DateTime CreatedAt);
