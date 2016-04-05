using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkit;
using unit_tests_csharp.P04NSubstitute.ProductionCode;

namespace unit_tests_csharp.P04NSubstitute
{
  public class Ex01BasicStubbingAndMocking
  {
    [Test]
    public void ShouldCopyDataFromSourceToDestination()
    {
      //GIVEN
      var copyOperation = new CopyOperation();
      var destination = Substitute.For<IDataDestination>();
      var source = Substitute.For<IDataSource>();
      var data = Any.Instance<Data>();

      source.RetrieveData().Returns(data);

      //WHEN
      copyOperation.ApplyTo(source, destination);

      //THEN
      destination.Received(1).Save(data);
      //destination.Received().Save(data);
    }
  }
}