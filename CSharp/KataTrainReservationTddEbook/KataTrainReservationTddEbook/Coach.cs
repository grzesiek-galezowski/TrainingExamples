namespace KataTrainReservationTddEbook
{
  public interface Coach
  {
    bool HasPreferredRoomFor(in uint seatCount);
    bool HasRoomFor(uint seatCount);
  }
}