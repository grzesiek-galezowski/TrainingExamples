using System.Data;
using System.Data.SQLite;
using System.Globalization;

namespace UnitTestRunnerPackageExercise;

public class SqLiteResultsDestination : ITestResultsDestination
{
  private readonly string _connectionString;

  public SqLiteResultsDestination(string connectionString)
  {
    _connectionString = connectionString;
  }

  public void Send(string entries)
  {
    using IDbConnection cnn = new SQLiteConnection(_connectionString);
    var dbCommand = cnn.CreateCommand();
    dbCommand.CommandText = "insert into Results (Date, Document) values (@Date, @Document)";
    dbCommand.Parameters["@Date"] = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
    dbCommand.Parameters["@Document"] = entries;
    dbCommand.ExecuteNonQuery();
  }
}