using System.Collections.Generic;
using System.Linq;

namespace KataTrainReservationTddEbook;

public class ReservableTrain : ITrain
{
  private readonly IReadOnlyList<ICoach> _coaches;
  private readonly uint _totalSeats;
  private readonly uint _reservedSeats;

  public ReservableTrain(IReadOnlyList<ICoach> coaches, uint totalSeats, uint reservedSeats)
  {
    _coaches = coaches;
    _totalSeats = totalSeats;
    _reservedSeats = reservedSeats;
  }

  public void UpdateIn(IFleet fleet)
  {
    throw new System.NotImplementedException();
  }

  public bool HasEnoughPlaceInAnyIndividualCoachFor(uint requestedSeatCount)
  {
    return _coaches.Any(c => c.HasPlaceFor(requestedSeatCount));
  }

  public bool HasTotalFreeSeatsAtLeast(uint requestedSeatCount)
  {
    var reservableSeats = _totalSeats * 0.7;
    var freeSeats = reservableSeats - _reservedSeats;
    return requestedSeatCount <= freeSeats;
  }

  public bool HasACoachThatWouldNotExceedTheSoftLimit(uint requestedSeatCount)
  {
    return _coaches.Any(c => c.HasPlaceWithingTheSoftLimitFor(requestedSeatCount));
  }

  public void BookSeatsInTheFirstCoachThatDoesNotExceedSoftLimitAfterBooking(
    uint requestedSeatCount,
    IReservationInProgress reservationInProgress)
  {
    _coaches.First(c => c.HasPlaceWithingTheSoftLimitFor(requestedSeatCount))
      .Reserve(requestedSeatCount, reservationInProgress);
  }

  public void BookSeatsInTheFirstCoachThatHasEnoughSeatsFor(uint requestedSeatCount,
    IReservationInProgress reservationInProgress)
  {
    _coaches.First(c => c.HasPlaceFor(requestedSeatCount))
      .Reserve(requestedSeatCount, reservationInProgress);
  }
}