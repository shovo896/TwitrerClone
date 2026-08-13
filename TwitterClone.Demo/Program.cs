using TwitterClone.Domain.Entities;
using TwitterClone.Domain.Notifications;
using TwitterClone.Domain.Shared;

var alice = new User("alice", "alice@example.com");
var bob = new User("bob", "bob@example.com", alice.Id);

var tweet = new Tweet(alice.Id, "Inheritance keeps shared entity data in one base class.");
var like = new Like(bob.Id, tweet.Id);
var retweet = new Retweet(bob.Id, tweet.Id);
var follow = new Follow(bob.Id, alice.Id);

Notification[] notifications =
[
    new LikeNotification(alice.Id, bob.Id, tweet.Id),
    new CommentNotification(alice.Id, bob.Id, tweet.Id, "Good explanation."),
    new FriendRequestNotification(alice.Id, bob.Id),
    new SystemNotification(bob.Id, "Welcome to TwitterClone.")
];

BaseEntity[] entities = [alice, bob, tweet, like, retweet, follow, .. notifications];

Console.WriteLine("TwitterClone runtime smoke test");
Console.WriteLine($"Users: {alice.Username}, {bob.Username}");
Console.WriteLine($"Tweet: {tweet.Describe()}");
Console.WriteLine($"Like Id: {like.Id}");
Console.WriteLine($"Retweet Id: {retweet.Id}");
Console.WriteLine($"Follow Id: {follow.Id}");
Console.WriteLine($"Entity count: {entities.Length}");

foreach (var notification in notifications)
{
    Console.WriteLine(notification.Describe());
}
