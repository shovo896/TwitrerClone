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
