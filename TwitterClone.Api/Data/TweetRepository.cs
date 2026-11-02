using System.Collections.Concurrent;
using TwitterClone.Api.Dtos;
using TwitterClone.Domain.Entities;

namespace TwitterClone.Api.Data;

public sealed class TweetRepository
{
    private readonly ConcurrentDictionary<Guid, TweetDto> _tweets = new();
    public IReadOnlyCollection<TweetDto> GetTweets(Guid? authorId = null) => _tweets.Values.Where(tweet => authorId is null || tweet.AuthorId == authorId).OrderByDescending(tweet => tweet.CreatedAt).ToArray();
    public TweetDto? GetTweetById(Guid id) => _tweets.GetValueOrDefault(id);
    public TweetDto AddTweet(CreateTweetRequest request) { var tweet = new Tweet(request.AuthorId, request.Content); var dto = ToDto(tweet); _tweets[dto.Id] = dto; return dto; }
    public TweetDto? UpdateTweet(Guid id, UpdateTweetRequest request) { if (!_tweets.TryGetValue(id, out var existing)) return null; var validated = new Tweet(existing.AuthorId, request.Content); var updated = new TweetDto(id, existing.AuthorId, validated.Content, existing.CreatedAt); _tweets[id] = updated; return updated; }
    public bool DeleteTweet(Guid id) => _tweets.TryRemove(id, out _);
    private static TweetDto ToDto(Tweet tweet) => new(tweet.Id, tweet.AuthorId, tweet.Content, tweet.CreatedAt);
}
