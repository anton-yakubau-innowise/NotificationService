public interface IReportPrinter
{
    void Print(string data);
}

public class ConsoleReportPrinter : IReportPrinter
{
    public void Print(string data)
    {
        Console.WriteLine("Report generated:");
        Console.WriteLine(data);
    }
}