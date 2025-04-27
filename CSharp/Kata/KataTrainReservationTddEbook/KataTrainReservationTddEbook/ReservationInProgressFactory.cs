namespace KataTrainReservationTddEbook;

public interface IReservationInProgressFactory
{
  IReservationInProgress FreshInstance();
}

public class DtoBasedReservationInProgressFactory : IReservationInProgressFactory
{
  public IReservationInProgress FreshInstance()
  {
    return new DtoBasedReservationInProgress();
  }
}

public class DtoBasedReservationInProgressFactorySpecification
{
  [Fact]
  public void ShouldDowhat()
  {
    //GIVEN
    var factory = new DtoBasedReservationInProgressFactory();
      
    //WHEN
    var instance = factory.FreshInstance();

    //THEN
    instance.Should().BeOfType<DtoBasedReservationInProgress>();
  }
}