using System;

namespace KataTrainReservationTddEbook
{
  public class DtoBasedReservationInProgress : ReservationInProgress
  {
    public ReservationDto ToDto()
    {
      throw new NotImplementedException();
    }

    public void NoRoomInTrainFor(uint seatCount)
    {
      throw new NotImplementedException();
    }
  }
}