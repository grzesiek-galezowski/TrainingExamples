using System;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using TddEbook.TddToolkit;
using unit_tests_csharp.P04NSubstitute.ProductionCode;

public class Ex03ThrowingExceptionsFromMocks
{

  [Test]
  public void ShouldThrowExceptionWhenReadingFromSourceThrowsException()
  {
    //GIVEN
    var copyOperation = new CopyOperation();
    var destination = Substitute.For<IDataDestination>();
    var source = Substitute.For<IDataSource>();
    var exception = Any.Exception();

    source.RetrieveData().Throws(exception);

    //WHEN - THEN
    var e = Assert.Throws<Exception>(
      () => copyOperation.ApplyTo(source, destination));
    Assert.AreEqual(exception, e);
    //never verification
    destination.DidNotReceive().Save(Arg.Any<Data>());
  }

  [Test]
  public void ShouldThrowExceptionWhenSavingToDestinationThrowsException()
  {
    //GIVEN
    CopyOperation copyOperation = new CopyOperation();
    IDataDestination destination = Substitute.For<IDataDestination>();
    IDataSource source = Substitute.For<IDataSource>();
    Data data = Any.Instance<Data>();
    Exception exception = Any.Exception();

    source.RetrieveData().Returns(data);
    destination.When(d => d.Save(data)).Throw(exception);

    //WHEN - THEN
    var e = Assert.Throws<Exception>(
      () => copyOperation.ApplyTo(source, destination));
    Assert.AreEqual(exception, e);
  }


}
