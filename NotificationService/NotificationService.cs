public class NotificationService
{
    public void SendNotification(IReceiver receiver, string message)
    {
        if (message.Length > 120)
        {
            throw new Exception("Msg120X");
        }

        receiver.Connect();
        receiver.Send(message);

        Console.WriteLine("Notification sent.");

    }

    public void SendPushNotification(string userId, string message)
    {
        if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
        {
            Console.WriteLine("SkipSendMode = true");
            return;
        }

        Console.WriteLine($"Connecting to Push Gateway...");
        Console.WriteLine($"Sending push to {userId}: {message}");
    }
}