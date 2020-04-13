namespace KataTrainReservationTddEbook
{
  public class TicketOffice
  {
    private readonly ReservationInProgressFactory _reservationInProgressFactory;
    private readonly CommandFactory _commandFactory;

    public TicketOffice(ReservationInProgressFactory reservationInProgressFactory, CommandFactory commandFactory)
    {
      _reservationInProgressFactory = reservationInProgressFactory;
      _commandFactory = commandFactory;
    }

    public ReservationDto MakeReservation(ReservationRequestDto requestDto)
    {
      var reservationInProgress = _reservationInProgressFactory.FreshInstance();
      var reservationCommand = _commandFactory.CreateReservationCommand(requestDto, reservationInProgress);
      reservationCommand.Execute();
      return reservationInProgress.ToDto();
    }
  }
}