public interface IReceiver
{
    void Connect();
    void Send(string message);
}

public abstract class EmailReceiver : IReceiver
{
    public string? Email { get; protected set; }

    public abstract void Connect();
    public void Send(string message)
    {
        Console.WriteLine($"Sending email to {Email}: {message}");
    }
}

public class AdminReceiver : EmailReceiver
{
    public string SmtpHost { get; private set; }

    public AdminReceiver()
    {
        Email = "admin@example.com";
        SmtpHost = "smtp.local";
    }

    public override void Connect()
    {
        Console.WriteLine("Connecting to SMTP...");
    }
}

public class UserReceiver : EmailReceiver
{
    public string SendGridApiKey { get; private set; }
    public UserReceiver()
    {
        Email = "user@example.com";
        SendGridApiKey = "123-456-789";
    }

    public override void Connect()
    {
        Console.WriteLine("Connecting to SendGrid...");
    }
}
