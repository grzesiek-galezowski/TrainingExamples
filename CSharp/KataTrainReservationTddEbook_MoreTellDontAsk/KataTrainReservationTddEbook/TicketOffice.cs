using KataTrainReservationTddEbook.Request;
using KataTrainReservationTddEbook.Response;

namespace KataTrainReservationTddEbook;

public class TicketOffice
{
  private readonly IReservationInProgressFactory _reservationInProgressFactory;
  private readonly ICommandFactory _commandFactory;

  public TicketOffice(IReservationInProgressFactory reservationInProgressFactory, ICommandFactory commandFactory)
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