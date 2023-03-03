using LiteDB;

namespace UnitTestRunnerPackageExercise;

public class LiteDbDestination : ITestResultsDestination
{
  private readonly string _fileName;

  public LiteDbDestination(string dbFileName)
  {
    _fileName = dbFileName;
  }

  public void Send(string entries)
  {
    using var db = new LiteDatabase(_fileName);
    var col = db.GetCollection<LiteDbResult>("results");

    col.Insert(new LiteDbResult
    {
      Document = entries,
      RunTime = DateTime.UtcNow
    });
  }
}

public class LiteDbResult
{
  public required DateTime RunTime { get; set; }
  public required string Document { get; set; }
}