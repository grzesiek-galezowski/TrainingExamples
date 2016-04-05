using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkit;
using unit_tests_csharp.P04NSubstitute.Ex010203ProductionCode;

namespace unit_tests_csharp.P04NSubstitute
{
  public class Ex02MultipleReturnsAndOrderVerification
  {
    [Test]
    public void ShouldReturnMultipleDataElements()
    {
      //GIVEN
      var source = Substitute.For<IDataSource>();
      var data1 = Any.Instance<Data>();
      var data2 = Any.Instance<Data>();

      source.RetrieveData().Returns(data1, data2);

      //WHEN
      var result1 = source.RetrieveData();
      var result2 = source.RetrieveData();

      //THEN
      Assert.AreEqual(data1, result1);
      Assert.AreEqual(data2, result2);
    }

    [Test] //order verification
    public void ShouldCopyDataFromSourceToDestinationMultipleTimes()
    {
      //GIVEN
      var copyOperation = new CopyOperation();
      var destination = Substitute.For<IDataDestination>();
      var source = Substitute.For<IDataSource>();
      var data1 = Any.Instance<Data>();
      var data2 = Any.Instance<Data>();


      source.RetrieveData().Returns(data1, data2);

      //WHEN
      copyOperation.ApplyTo(source, destination);
      copyOperation.ApplyTo(source, destination);

      //THEN
      Received.InOrder(() =>
      {
        destination.Save(data1);
        destination.Save(data2);
      });
    }
  }
}
