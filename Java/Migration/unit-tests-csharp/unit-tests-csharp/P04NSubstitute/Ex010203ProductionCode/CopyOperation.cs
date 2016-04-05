namespace unit_tests_csharp.P04NSubstitute.Ex010203ProductionCode
{
  public class CopyOperation
  {
    public void ApplyTo(IDataSource source, IDataDestination destination)
    {
      var data = source.RetrieveData();
      destination.Save(data);
    }
  }
}