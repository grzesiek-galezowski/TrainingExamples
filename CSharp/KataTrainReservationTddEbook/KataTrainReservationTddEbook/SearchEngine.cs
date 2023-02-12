using System.Collections.Generic;
using Functional.Maybe;

namespace KataTrainReservationTddEbook;

public interface ISearchEngine
{
  Maybe<ICoach> FindCoachForReservation(IEnumerable<ICoach> coaches, in uint seatCount);
}