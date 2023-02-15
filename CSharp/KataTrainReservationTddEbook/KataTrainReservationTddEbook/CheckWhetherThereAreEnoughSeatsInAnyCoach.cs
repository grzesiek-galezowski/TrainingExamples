using Microsoft.VisualBasic.FileIO;

namespace KataTrainReservationTddEbook;

//bug is this step needed?
public class CheckWhetherThereAreEnoughSeatsInAnyCoach : IBookingStep
{
  private readonly uint _requestedSeatCount;
  private readonly IBookingStep _next;

  public CheckWhetherThereAreEnoughSeatsInAnyCoach(uint requestedSeatCount, IBookingStep next)
  {
    _requestedSeatCount = requestedSeatCount;
    _next = next;
  }

  public void Invoke(ITrain train, IReservationInProgress reservationInProgress)
  {
    if (train.HasEnoughPlaceInAnyIndividualCoachFor(_requestedSeatCount))
    {
      _next.Invoke(train, reservationInProgress);
    }
    else
    {
      reservationInProgress.NotEnoughSeatsInAnyCoachToFit(_requestedSeatCount);
    }
  }
}