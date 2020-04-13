using System.Collections.Generic;
using Functional.Maybe;

namespace KataTrainReservationTddEbook
{
  public interface SearchEngine
  {
    Maybe<Coach> FindCoachForReservation(IEnumerable<Coach> coaches, in uint seatCount);
  }
}