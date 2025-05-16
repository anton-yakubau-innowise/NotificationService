public class ReportService
{
    private readonly IReportPrinter _printer;
    private readonly IReportDataProvider _dataProvider;
    private readonly IReportArchiver _archiver;

    public ReportService(IReportPrinter printer, IReportDataProvider dataProvider, IReportArchiver archiver)
    {
        _printer = printer;
        _dataProvider = dataProvider;
        _archiver = archiver;
    }

    public void GenerateWeeklyReport(string reportContext)
    {
        var data = _dataProvider.FetchReportData(reportContext);
        _printer.Print(data);
    }

    public void ArchiveWeeklyReport()
    {
        _archiver.ArchiveOldReports();
    }
}