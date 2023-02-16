using System;
using KataTrainReservationTddEbook.Response;

namespace KataTrainReservationTddEbook;

public class DtoBasedReservationInProgress : IReservationInProgress
{
  public ReservationDto ToDto()
  {
    throw new NotImplementedException();
  }

  public void NoRoomInTrainFor(in uint seatCount)
  {
    throw new NotImplementedException();
  }

  public void NotEnoughSeatsInAnyCoachToFit(in uint seatCount)
  {
    throw new NotImplementedException();
  }

  public void ReservedSeat(string bookingReference, string name)
  {
    throw new NotImplementedException();
  }
}