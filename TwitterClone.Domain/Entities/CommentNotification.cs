namespace Twitter_Clone.Entities;

public class CommentNotification : TwitterClone.Domain.Notifications.Notification
{
    public CommentNotification(Guid commentByUserId) : base(commentByUserId, commentByUserId)
    {
        CommentByUserId = commentByUserId;
    }

    public Guid CommentByUserId { get; set; }
    public override string Type => "Comment";
    public string Message { get; private set; } = string.Empty;

    public override string GetMessage() => Message;

    public void AddMessage(string message)
    {
        Message = message;
    }
}
