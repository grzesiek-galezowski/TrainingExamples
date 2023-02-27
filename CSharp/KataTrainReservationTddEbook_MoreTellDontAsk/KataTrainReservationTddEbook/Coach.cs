using System.Collections.Generic;
using System.Linq;

namespace KataTrainReservationTddEbook;

public interface ICoach
{
  bool HasPlaceFor(uint requestedSeatCount);
  bool HasPlaceWithingTheSoftLimitFor(uint requestedSeatCount);
  void Reserve(uint requestedSeatCount, IReservationInProgress reservationInProgress);
}

public class ReservableCoach : ICoach
{
  private readonly IReadOnlyList<ISeat> _seats;
  private readonly IReservationReferenceService _referenceService;

  public ReservableCoach(IReadOnlyList<ISeat> seats, IReservationReferenceService referenceService)
  {
    _seats = seats;
    _referenceService = referenceService;
  }

  public bool HasPlaceFor(uint requestedSeatCount)
  {
    return _seats.Count(s => s.IsFree()) >= requestedSeatCount;
  }

  public bool HasPlaceWithingTheSoftLimitFor(uint requestedSeatCount)
  {
    return _seats.Count(s => !s.IsFree()) + requestedSeatCount <= _seats.Count * 0.7;
  }

  public void Reserve(uint requestedSeatCount, IReservationInProgress reservationInProgress)
  {
    var bookingReference = _referenceService.GetBookingReference();
    var freeSeats = _seats.Where(s => s.IsFree()).ToList();
    for (int i = 0; i < requestedSeatCount; i++)
    {
      freeSeats[i].Reserve(bookingReference, reservationInProgress);
    }
  }
}