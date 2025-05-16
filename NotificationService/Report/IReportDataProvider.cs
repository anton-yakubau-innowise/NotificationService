public interface IReportDataProvider
{
    string FetchReportData(string reportContext);
}

public class ReportDataProvider : IReportDataProvider
{
    public string FetchReportData(string reportContext)
    {
        //Pretend that reportContext is used for something
        return $"Weekly report data: ....";
    }
}