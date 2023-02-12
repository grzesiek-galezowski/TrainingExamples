namespace KataTrainReservationTddEbook;

public interface CommandFactory
{
  ReservationCommand CreateReservationCommand(ReservationRequestDto requestDto, ReservationInProgress reservationInProgress);
}