namespace KataTrainReservationTddEbook;

public class CheckWhetherThereAreEnoughSeatsInTheTrain : IBookingStep
{
  private readonly uint _requestedSeatCount;
  private readonly IBookingStep _next;

  public CheckWhetherThereAreEnoughSeatsInTheTrain(uint requestedSeatCount, IBookingStep next)
  {
    _requestedSeatCount = requestedSeatCount;
    _next = next;
  }

  public void Invoke(ITrain train, IReservationInProgress reservationInProgress)
  {
    if (train.HasTotalFreeSeatsAtLeast(_requestedSeatCount))
    {
      _next.Invoke(train, reservationInProgress);
    }
    else
    {
      reservationInProgress.NoRoomInTrainFor(_requestedSeatCount);
    }
  }
}