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

  public void NoMatchingCoachFoundFor(in uint seatCount)
  {
    throw new NotImplementedException();
  }
}