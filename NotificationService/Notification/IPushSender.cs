public interface IPushSender
{
    void SendPush(string userId, string message);
}

public class MondayToSaturdayPushSender : IPushSender
{
    public void SendPush(string userId, string message)
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