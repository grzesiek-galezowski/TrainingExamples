namespace KataTrainReservationTddEbook
{
  public interface Train
  {
    bool HasRoomInPreferredCoachFor(uint seatCount);
    void ReserveSeatsInPreferredCoach(uint seatCount, ReservationInProgress reservationInProgress);
    bool HasRoomInAnyCoachFor(uint seatCount);
    void ReserveSeatsInAnyFreeCoach(uint seatCount, ReservationInProgress reservationInProgress);
  }
}