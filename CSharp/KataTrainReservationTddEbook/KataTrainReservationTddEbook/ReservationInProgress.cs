namespace KataTrainReservationTddEbook;

public interface ReservationInProgress
{
  ReservationDto ToDto();
  void NoRoomInTrainFor(in uint seatCount);
  void NoMatchingCoachFoundFor(in uint seatCount);
}