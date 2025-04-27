namespace KataTrainReservationTddEbook;

public class TodoReservationInProgressFactory : IReservationInProgressFactory
{
  public IReservationInProgress FreshInstance()
  {
    return new DtoBasedReservationInProgress();
  }
}