namespace TwitterClone.Domain.Entities;

public interface IFollowable
{
    void Follow(Guid userId);
    void Unfollow(Guid userId);
}



// This interface defines the contract for entities that can be followed or unfollowed by users.