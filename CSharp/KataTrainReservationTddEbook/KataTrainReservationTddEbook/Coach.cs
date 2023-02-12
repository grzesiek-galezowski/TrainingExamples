namespace KataTrainReservationTddEbook;

public interface ICoach
{
  void Reserve(in uint seatCount, IReservationInProgress reservationInProgress);
  uint GetPercentageReserved();
}

public class ReservableCoach : ICoach
{
  public void Reserve(in uint seatCount, IReservationInProgress reservationInProgress)
  {
    throw new System.NotImplementedException();
  }

  public uint GetPercentageReserved()
  {
    throw new System.NotImplementedException();
  }
}