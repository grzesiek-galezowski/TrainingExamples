namespace KataTrainReservationTddEbook;

public class MakeReservation : IBookingStep
{
  private readonly uint _requestedSeatCount;

  public MakeReservation(uint requestedSeatCount)
  {
    _requestedSeatCount = requestedSeatCount;
  }

  public void Invoke(ITrain train, IReservationInProgress reservationInProgress)
  {
    if (train.HasACoachThatWouldNotExceedTheSoftLimit(_requestedSeatCount))
    {
      train.BookSeatsInTheFirstCoachThatDoesNotExceedSoftLimitAfterBooking(_requestedSeatCount, reservationInProgress);
    }
    else
    {
      train.BookSeatsInTheFirstCoachThatHasEnoughSeatsFor(_requestedSeatCount, reservationInProgress);
    }
  }
}