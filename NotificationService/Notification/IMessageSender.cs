public interface IMessageSender
{
    void SendMessage(string message);
}

public class EmailSender : IMessageSender
{
    private readonly IReceiver _receiver;

    public EmailSender(IReceiver receiver)
    {
        _receiver = receiver;
    }

    public void SendMessage(string message)
    {
        _receiver.Connect();
        _receiver.Send(message);
    }
}
