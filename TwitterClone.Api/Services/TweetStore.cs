using System.Collections.Concurrent;
using TwitterClone.Api.Models;

namespace TwitterClone.Api.Services;

public sealed class TweetStore
{
    private readonly ConcurrentDictionary<int, TweetResponse> _tweets = new();
    private int _nextId = 2;

    public TweetStore()
    {
        AddSeed(new TweetResponse(1, "Welcome to TwitterClone!", "Shuvo", "@shuvo", DateTime.UtcNow));
        AddSeed(new TweetResponse(2, "Learning ASP.NET Core Web API.", "CPS Academy", "@cpsacademy", DateTime.UtcNow));
    }

    public IReadOnlyCollection<TweetResponse> GetAll() =>
        _tweets.Values.OrderByDescending(tweet => tweet.CreatedAt).ToArray();

    public TweetResponse? GetById(int id) => _tweets.GetValueOrDefault(id);

    public TweetResponse Create(CreateTweetRequest request)
    {
        var id = Interlocked.Increment(ref _nextId);
        var tweet = new TweetResponse(id, request.Text.Trim(), request.Author.Trim(), request.Handle, DateTime.UtcNow);
        _tweets[id] = tweet;
        return tweet;
    }

    private void AddSeed(TweetResponse tweet) => _tweets[tweet.Id] = tweet;
}
