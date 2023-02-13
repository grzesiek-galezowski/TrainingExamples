using System.Collections.Generic;
using System.Linq;

namespace KataTrainReservationTddEbook;

public class ReservableTrain : ITrain
{
  private readonly IEnumerable<ICoach> _coaches;

  public ReservableTrain(IEnumerable<ICoach> coaches)
  {
    _coaches = coaches;
  }

  public const uint UpperBound = 10; //bug

  public void Reserve(in uint seatCount, ISearchEngine searchEngine, IReservationInProgress reservationInProgress)
  {
    var chosenCoach = searchEngine.FindCoachForReservation(_coaches, seatCount);
    if (chosenCoach.HasValue)
    {
      chosenCoach.Value.Reserve(seatCount, reservationInProgress);
    }
    else
    {
      reservationInProgress.NoMatchingCoachFoundFor(seatCount);
    }
  }

  public bool HasCapacityForReservationsInAdvance()
  {
    var percentages = _coaches.Select(c => c.GetPercentageReserved());
    if (percentages.Average(p => p) <= UpperBound)
    {
      return true;
    }
    return false;
  }
}