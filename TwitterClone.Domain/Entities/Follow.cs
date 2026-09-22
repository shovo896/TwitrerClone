namespace TwitterClone.Domain.Entities;

using TwitterClone.Domain.Shared;

public class Follow : BaseEntity
{
    public Follow(Guid followerId, Guid followeeId) : base(followerId)
    {
        if (followerId == Guid.Empty) throw new ArgumentException("Follower is required.", nameof(followerId));
        if (followeeId == Guid.Empty) throw new ArgumentException("Followee is required.", nameof(followeeId));
        if (followerId == followeeId) throw new ArgumentException("A user cannot follow themselves.", nameof(followeeId));

        FollowerId = followerId;
        FolloweeId = followeeId;
    }

    public Guid FollowerId { get; }
    public Guid FolloweeId { get; }
}
