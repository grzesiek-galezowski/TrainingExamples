using KataTrainReservationTddEbook.Request;

namespace KataTrainReservationTddEbook;

public interface ICommandFactory
{
  IReservationCommand CreateReservationCommand(ReservationRequestDto requestDto, IReservationInProgress reservationInProgress);
}