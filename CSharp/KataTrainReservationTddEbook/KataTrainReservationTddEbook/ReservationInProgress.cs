namespace KataTrainReservationTddEbook
{
  public interface ReservationInProgress
  {
    ReservationDto ToDto();
    void NoRoomInTrainFor(uint seatCount);
  }
}