namespace TwitterClone.Domain.Entities;

using TwitterClone.Domain.Shared;
using TwitterClone.Domain.Notifications;

public class User : BaseEntity, IFollowable, INotifiable
{
    private string _username = string.Empty;
    private string _email = string.Empty;
    private readonly List<Guid> _following = [];
    private readonly List<Notification> _unreadNotifications = [];

    public User(string username, string email, Guid? createdBy = null) : base(createdBy)
    {
        Username = username;
        Email = email;
    }

    public string Username
    {
        get => _username;
        private set => _username = ValidateRequired(value, nameof(Username));
    }

    public string Email
    {
        get => _email;
        private set => _email = ValidateRequired(value, nameof(Email));
    }

    public IReadOnlyCollection<Guid> Following => _following.AsReadOnly();
    public IReadOnlyCollection<Notification> UnreadNotifications => _unreadNotifications.AsReadOnly();

    public void Follow(Guid userId)
    {
        if (userId == Guid.Empty) throw new ArgumentException("User is required.", nameof(userId));
        if (userId == Id) throw new ArgumentException("A user cannot follow themselves.", nameof(userId));

        if (!_following.Contains(userId))
            _following.Add(userId);
    }

    public void Unfollow(Guid userId)
    {
        _following.Remove(userId);
    }

    public void AddNotification(Notification notification)
    {
        ArgumentNullException.ThrowIfNull(notification);

        if (!_unreadNotifications.Contains(notification))
            _unreadNotifications.Add(notification);
    }

    public void ReadNotification(Notification notification)
    {
        ArgumentNullException.ThrowIfNull(notification);
        _unreadNotifications.Remove(notification);
    }

    private static string ValidateRequired(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{fieldName} is required.", fieldName);

        return value.Trim();
    }
}
