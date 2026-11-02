using System.Collections.Concurrent;
using TwitterClone.Api.Models;

namespace TwitterClone.Api.Services;

public sealed class SocialStore
{
    public static readonly Guid DemoUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    private readonly ConcurrentDictionary<Guid, SocialUser> _users = new();
    private readonly ConcurrentDictionary<Guid, MessageResponse> _messages = new();
    private readonly ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, byte>> _likes = new();
    private readonly ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, byte>> _bookmarks = new();
    private readonly ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, byte>> _follows = new();
    private readonly ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, byte>> _retweets = new();

    public SocialStore() => _users[DemoUserId] = new SocialUser(DemoUserId, "Shuvo", "shuvo@example.com");
    public IReadOnlyCollection<SocialUser> Users => _users.Values.ToArray();
    public SocialUser? GetUser(Guid id) => _users.GetValueOrDefault(id);

    public SocialUser AddUser(string username, string email)
    {
        var user = new SocialUser(Guid.NewGuid(), username.Trim(), email.Trim());
        _users[user.Id] = user;
        return user;
    }

    public SocialUser? UpdateUser(Guid id, string username, string email)
    {
        if (!_users.TryGetValue(id, out var existing)) return null;
        var updated = existing with { Username = username.Trim(), Email = email.Trim() };
        _users[id] = updated;
        return updated;
    }

    public SocialUser? UpdatePhone(Guid id, string phoneNumber)
    {
        if (!_users.TryGetValue(id, out var user)) return null;
        var updated = user with { PhoneNumber = phoneNumber.Trim() };
        _users[id] = updated;
        return updated;
    }

    public bool RemoveUser(Guid id) => _users.TryRemove(id, out _);

    public void ToggleLike(Guid userId, Guid tweetId, out bool liked)
    {
        var users = _likes.GetOrAdd(tweetId, _ => new ConcurrentDictionary<Guid, byte>());
        liked = users.TryAdd(userId, 0);
        if (!liked) users.TryRemove(userId, out _);
    }

    public int LikeCount(Guid tweetId) => _likes.GetValueOrDefault(tweetId)?.Count ?? 0;
    public bool HasLiked(Guid userId, Guid tweetId) => _likes.GetValueOrDefault(tweetId)?.ContainsKey(userId) == true;

    public void ToggleBookmark(Guid userId, Guid tweetId, out bool bookmarked)
    {
        var users = _bookmarks.GetOrAdd(tweetId, _ => new ConcurrentDictionary<Guid, byte>());
        bookmarked = users.TryAdd(userId, 0);
        if (!bookmarked) users.TryRemove(userId, out _);
    }

    public bool HasBookmark(Guid userId, Guid tweetId) => _bookmarks.GetValueOrDefault(tweetId)?.ContainsKey(userId) == true;
    public IReadOnlyCollection<Guid> GetBookmarks(Guid userId) => _bookmarks.Where(pair => pair.Value.ContainsKey(userId)).Select(pair => pair.Key).ToArray();
    public void RemoveBookmarks(Guid userId) { foreach (var bookmark in _bookmarks.Values) bookmark.TryRemove(userId, out _); }

    public void ToggleFollow(Guid followerId, Guid followeeId, out bool following)
    {
        if (followerId == followeeId) throw new ArgumentException("A user cannot follow themselves.");
        var users = _follows.GetOrAdd(followeeId, _ => new ConcurrentDictionary<Guid, byte>());
        following = users.TryAdd(followerId, 0);
        if (!following) users.TryRemove(followerId, out _);
    }

    public bool IsFollowing(Guid followerId, Guid followeeId) => _follows.GetValueOrDefault(followeeId)?.ContainsKey(followerId) == true;

    public void ToggleRetweet(Guid userId, Guid tweetId, out bool retweeted)
    {
        var users = _retweets.GetOrAdd(tweetId, _ => new ConcurrentDictionary<Guid, byte>());
        retweeted = users.TryAdd(userId, 0);
        if (!retweeted) users.TryRemove(userId, out _);
    }

    public bool HasRetweet(Guid userId, Guid tweetId) => _retweets.GetValueOrDefault(tweetId)?.ContainsKey(userId) == true;

    public MessageResponse AddMessage(Guid senderId, SendMessageRequest request)
    {
        var message = new MessageResponse(Guid.NewGuid(), senderId, request.RecipientId, request.Text.Trim(), false, DateTime.UtcNow);
        _messages[message.Id] = message;
        return message;
    }

    public IReadOnlyCollection<MessageResponse> GetMessages(Guid userId) => _messages.Values.Where(message => message.SenderId == userId || message.RecipientId == userId).OrderByDescending(message => message.CreatedAt).ToArray();
    public MessageResponse? GetMessage(Guid id) => _messages.GetValueOrDefault(id);
    public bool DeleteMessage(Guid id) => _messages.TryRemove(id, out _);

    public bool MarkMessageRead(Guid id)
    {
        if (!_messages.TryGetValue(id, out var message)) return false;
        _messages[id] = message with { IsRead = true };
        return true;
    }

    public void DeleteMessages(Guid userId) { foreach (var message in GetMessages(userId)) _messages.TryRemove(message.Id, out _); }
}
