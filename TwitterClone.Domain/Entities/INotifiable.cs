namespace TwitterClone.Domain.Entities;

using TwitterClone.Domain.Notifications;

public interface INotifiable
{
    void AddNotification(Notification notification);
    void ReadNotification(Notification notification);
}
