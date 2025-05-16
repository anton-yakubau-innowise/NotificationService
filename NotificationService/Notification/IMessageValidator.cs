public interface IMessageValidator
{
    void Validate(string message);
}

public class MaxLenghtValidator : IMessageValidator
{
    private const int MaxLength = 120;

    public void Validate(string message)
    {
        if (message.Length > MaxLength)
        {
            throw new Exception($"Message cannot exceed {MaxLength} characters.");
        }
    }
}
