using KataTrainReservationTddEbook.Response;

namespace KataTrainReservationTddEbook;

public interface IReservationInProgress
{
  ReservationDto ToDto();
  void NoRoomInTrainFor(in uint seatCount);
  void NoMatchingCoachFoundFor(in uint seatCount);
}