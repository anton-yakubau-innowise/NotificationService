public class ReportService
{
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