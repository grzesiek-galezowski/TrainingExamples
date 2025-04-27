using KataTrainReservationTddEbook.Request;

namespace KataTrainReservationTddEbook;

public class TicketOfficeCommandFactory : ICommandFactory
{
  private readonly TrainDataService _trainDataService;
  private readonly TrainHardLimitReservabilityVisitor _trainHardLimitReservabilityVisitor;

  public TicketOfficeCommandFactory(
    TrainDataService trainDataService,
    TrainHardLimitReservabilityVisitor trainHardLimitReservabilityVisitor)
  {
    _trainDataService = trainDataService;
    _trainHardLimitReservabilityVisitor = trainHardLimitReservabilityVisitor;
  }

  public IReservationCommand CreateReservationCommand(ReservationRequestDto requestDto,
    IReservationInProgress reservationInProgress)
  {
    return new TrainReservationCommand(
      new TrainId(requestDto.TrainId), 
      _trainDataService, 
      reservationInProgress,
      requestDto.SeatCount, 
      _trainHardLimitReservabilityVisitor);
  }
}