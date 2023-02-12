using System.Collections.Generic;
using Functional.Maybe;

namespace KataTrainReservationTddEbook;

public class PercentageBasedSearchEngine : ISearchEngine
{
  public Maybe<ICoach> FindCoachForReservation(IEnumerable<ICoach> coaches, in uint seatCount)
  {
    throw new System.NotImplementedException();
  }
}