public interface IReceiver
{
    void Connect();
    void Send(string message);

}

public class AdminReceiver : IReceiver
{
    public string Email { get; private set; }
    public string SmtpHost { get; private set; }

    public AdminReceiver()
    {
        Email = "admin@example.com";
        SmtpHost = "smtp.local";
    }

    public void Connect()
    {
        Console.WriteLine("Connecting to SMTP...");
    }

    public void Send(string message)
    {
        Console.WriteLine($"Sending email to {Email}: {message}");
    }
}

public class UserReceiver : IReceiver
{
    public string Email { get; private set; }
    public string SendGridApiKey { get; private set; }
    public UserReceiver()
    {
        Email = "user@example.com";
        SendGridApiKey = "123-456-789";
    }

    public void Connect()
    {
        Console.WriteLine("Connecting to SendGrid...");
    }

    public void Send(string message)
    {
        Console.WriteLine($"Sending email to {Email}: {message}");
    }
}
