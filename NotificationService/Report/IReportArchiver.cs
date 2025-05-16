public interface IReportArchiver
{
    void ArchiveOldReports();
}

public class DefaultReportArchiver : IReportArchiver
{
    public void ArchiveOldReports()
    {
        Console.WriteLine("Archiving reports older than 30 days...");
        // pretend to do archiving
    }
}