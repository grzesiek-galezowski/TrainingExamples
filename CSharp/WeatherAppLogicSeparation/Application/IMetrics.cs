namespace Application;

public interface IMetrics
{
  Task ReportException(Exception exception);
}