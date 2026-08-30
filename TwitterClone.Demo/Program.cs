using TwitterClone.Domain.Entities;
using TwitterClone.Domain.Notifications;
using TwitterClone.Domain.Shared;

var alice = new User("alice", "alice@example.com");
var bob = new User("bob", "bob@example.com", alice.Id);

var tweet = new Tweet(alice.Id, "Inheritance keeps shared entity data in one base class.");
var comment = new Comment(bob.Id, tweet.Id, "Polymorphism works through shared interfaces.");
var like = new Like(bob.Id, tweet.Id);
var retweet = new Retweet(bob.Id, tweet.Id);
var follow = new Follow(bob.Id, alice.Id);

Notification[] notifications =
[
    new LikeNotification(alice.Id, bob.Id, tweet.Id),
    new CommentNotification(alice.Id, bob.Id, tweet.Id, "Good explanation."),
    new FriendRequestNotification(alice.Id, bob.Id),
    new MentionNotification(alice.Id, bob.Id, tweet.Id),
    new SystemNotification(bob.Id, "Welcome to TwitterClone.")
];

BaseEntity[] entities = [alice, bob, tweet, comment, like, retweet, follow, .. notifications];
ILikeable[] likeableItems = [tweet, comment];

bob.Follow(alice.Id);
bob.AddNotification(notifications[4]);

Console.WriteLine("TwitterClone runtime smoke test");
Console.WriteLine($"Users: {alice.Username}, {bob.Username}");
Console.WriteLine($"Tweet: {tweet.Describe()}");
Console.WriteLine($"Comment: {comment.Describe()}");
Console.WriteLine($"Like Id: {like.Id}");
Console.WriteLine($"Retweet Id: {retweet.Id}");
Console.WriteLine($"Follow Id: {follow.Id}");
Console.WriteLine($"Entity count: {entities.Length}");
Console.WriteLine($"Bob following count: {bob.Following.Count}");
Console.WriteLine($"Bob unread notifications: {bob.UnreadNotifications.Count}");

foreach (var item in likeableItems)
{
    Console.WriteLine($"{item.GetType().Name} can be liked: {item.CanBeLiked()}");
}

foreach (var notification in notifications)
{
    Console.WriteLine(notification.Describe());
    Console.WriteLine(notification.GetMessage());
}
