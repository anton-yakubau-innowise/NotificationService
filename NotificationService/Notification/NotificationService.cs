public class NotificationService
{
    private readonly IMessageValidator _messageValidator;
    
    public NotificationService(IMessageValidator messageValidator)
    {
        _messageValidator = messageValidator;
    }

    public void SendNotification(IMessageSender sender, string message)
    {
        _messageValidator.Validate(message);

        sender.SendMessage(message);

        Console.WriteLine("Notification sent.");
    }

    public void SendPushNotification(IPushSender pushSender, string userId, string message)
    {
        pushSender.SendPush(userId, message);

        Console.WriteLine("Push notification sent.");
    }
}