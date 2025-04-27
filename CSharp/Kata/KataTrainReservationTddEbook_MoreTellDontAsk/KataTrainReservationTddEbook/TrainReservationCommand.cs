namespace KataTrainReservationTddEbook;

public class TrainReservationCommand : IReservationCommand
{
  private readonly IFleet _fleet;
  private readonly IReservationInProgress _reservationInProgress;
  private readonly TrainId _trainId;
  private readonly uint _requestedSeatCount;
  private TrainHardLimitReservabilityVisitor _trainHardLimitReservabilityVisitor;

  public TrainReservationCommand(
    TrainId trainId,
    IFleet fleet,
    IReservationInProgress reservationInProgress, 
    uint requestedSeatCount, 
    TrainHardLimitReservabilityVisitor trainHardLimitReservabilityVisitor)
  {
    _fleet = fleet;
    _reservationInProgress = reservationInProgress;
    _requestedSeatCount = requestedSeatCount;
    _trainHardLimitReservabilityVisitor = trainHardLimitReservabilityVisitor;
    _trainId = trainId;
  }

  public void Execute()
  {
    var train = _fleet.RetrieveBy(_trainId);
    train.EvaluateUpFrontTrainReservability(
      _requestedSeatCount,
      _reservationInProgress,
      _trainHardLimitReservabilityVisitor);
    train.UpdateIn(_fleet);
  }
}