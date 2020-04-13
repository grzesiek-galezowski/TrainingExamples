using System;

namespace KataTrainReservationTddEbook
{
  public class DtoBasedReservationInProgress : ReservationInProgress
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
}