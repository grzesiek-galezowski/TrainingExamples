namespace KataTrainReservationTddEbook;

public class CheckThereAreEnoughSeatsInTheTrain : IBookingStep
{
  private readonly uint _requestedSeatCount;

  public CheckThereAreEnoughSeatsInTheTrain(uint requestedSeatCount)
  {
    _requestedSeatCount = requestedSeatCount;
  }

  public void Invoke(ITrain train, IReservationInProgress reservationInProgress)
  {
    if (train.HasLessTotalPlacesThan(_requestedSeatCount))
    {
      reservationInProgress.NoRoomInTrainFor(_requestedSeatCount);
    }
    else
    {
      _next.Invoke(train, reservationInProgress);
    }
  }
}