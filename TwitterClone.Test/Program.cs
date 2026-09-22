using TwitterClone.Domain.Entities;
using TwitterClone.Domain.Notifications;

var recipientId = Guid.NewGuid();
var actorId = Guid.NewGuid();
var tweetId = Guid.NewGuid();

List<Notification> notifications =
[
    new LikeNotification(recipientId, actorId, tweetId),
    new CommentNotification(recipientId, actorId, tweetId, "Nice post."),
    new FriendRequestNotification(recipientId, actorId),
    new MentionNotification(recipientId, actorId, tweetId),
    new SystemNotification(recipientId, "System maintenance starts tonight.")
];

foreach (var notification in notifications)
{
    Console.WriteLine(notification.GetNotificationInfo());
    Console.WriteLine(notification.GetMessage());
}

List<ILikeable> likeableItems =
[
    new Tweet(actorId, "Polymorphism lets different objects share one behavior contract."),
    new Comment(actorId, tweetId, "Interface based behavior for comments.")
];

foreach (var item in likeableItems)
{
    Console.WriteLine($"{item.GetType().Name} can be liked: {item.CanBeLiked()}");
}

var user = new User("shuvo", "shuvo@example.com");
user.Follow(actorId);
user.AddNotification(notifications[0]);

Console.WriteLine($"Following count: {user.Following.Count}");
Console.WriteLine($"Unread notifications: {user.UnreadNotifications.Count}");
