namespace XUnitTestPatterns._06_PickingTestValues
{
  public class BackupFileNamePattern
  {
    public string ApplyTo(string hostName, string userName)
    {
      return $"backup_{hostName}_{userName}.zip";
    }
  }
}