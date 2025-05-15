public class NotificationService
{
    private string adminEmail = "admin@example.com";
    private string userEmail = "user@example.com";
    private string smtpHost = "smtp.local";
    private string sendGridApiKey = "123-456-789";
    
    public void SendNotification(string type, string message)
    {
        if (message.Length > 120)
        {
            throw new Exception("Msg120X");
        }

        if (type == "Admin")
        {
            Console.WriteLine("Connecting to SMTP...");
            Console.WriteLine($"Sending email to {adminEmail}: {message}");
        }
        else if (type == "User")
        {
            Console.WriteLine("Connecting to SendGrid...");
            Console.WriteLine($"Sending email to {userEmail}: {message}");
        }
        else if (type == "Push")
        {
            Console.WriteLine("Push Notification not implemented.");
        }
        else
        {
            throw new ArgumentException("Unknown notification type.");
        }

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

    public void GenerateWeeklyReport()
    {
        var data = FetchReportData();
        Console.WriteLine("Report generated:");
        Console.WriteLine(data);
    }

    public void ArchiveWeeklyReport()
    {
        Console.WriteLine("Archiving reports older than 30 days...");
        // pretend to do archiving
    }

    private string FetchReportData()
    {
        return "Weekly report data (hardcoded)";
    }
}