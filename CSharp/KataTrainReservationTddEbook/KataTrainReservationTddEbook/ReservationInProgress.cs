using KataTrainReservationTddEbook.Response;

namespace KataTrainReservationTddEbook;

public interface IReservationInProgress
{
  ReservationDto ToDto();
  void NoRoomInTrainFor(in uint seatCount);
  void NotEnoughSeatsInAnyCoachToFit(in uint seatCount);
  void ReservedSeat(string bookingReference, string name); //bug SeatId?
}