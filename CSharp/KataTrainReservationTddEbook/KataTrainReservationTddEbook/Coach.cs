namespace KataTrainReservationTddEbook
{
  public interface Coach
  {
    void Reserve(in uint seatCount, ReservationInProgress reservationInProgress);
  }
}