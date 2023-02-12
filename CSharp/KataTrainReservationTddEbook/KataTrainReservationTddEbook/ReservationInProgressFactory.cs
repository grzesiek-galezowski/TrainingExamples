using FluentAssertions;
using Xunit;

namespace KataTrainReservationTddEbook;

public interface ReservationInProgressFactory
{
  ReservationInProgress FreshInstance();
}

public class DtoBasedReservationInProgressFactory : ReservationInProgressFactory
{
  public ReservationInProgress FreshInstance()
  {
    return new DtoBasedReservationInProgress();
  }
}

public class DtoBasedReservationInProgressFactorySpecification
{
  [Fact]
  public void ShouldDOWHAT()
  {
    //GIVEN
    var factory = new DtoBasedReservationInProgressFactory();
      
    //WHEN
    var instance = factory.FreshInstance();

    //THEN
    instance.Should().BeOfType<DtoBasedReservationInProgress>();
  }
}