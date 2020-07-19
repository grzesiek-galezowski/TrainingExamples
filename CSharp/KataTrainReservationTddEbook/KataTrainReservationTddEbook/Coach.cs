namespace KataTrainReservationTddEbook
{
  public interface Coach
  {
    void Reserve(in uint seatCount, ReservationInProgress reservationInProgress);
    uint GetPercentageReserved();
  }

  public class ReservableCoach : Coach
  {
    public void Reserve(in uint seatCount, ReservationInProgress reservationInProgress)
    {
      throw new System.NotImplementedException();
    }

    public uint GetPercentageReserved()
    {
      throw new System.NotImplementedException();
    }
  }
}