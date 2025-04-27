namespace KataTrainReservationTddEbook;

public class TrainReservationCommand : IReservationCommand
{
  private readonly IFleet _fleet;
  private readonly IReservationInProgress _reservationInProgress;
  private readonly TrainId _trainId;
  private readonly IBookingProcess _bookingProcess;

  public TrainReservationCommand(
    TrainId trainId,
    IFleet fleet,
    IReservationInProgress reservationInProgress, 
    IBookingProcess bookingProcess)
  {
    _fleet = fleet;
    _reservationInProgress = reservationInProgress;
    _bookingProcess = bookingProcess;
    _trainId = trainId;
  }

  public void Execute()
  {
    var train = _fleet.RetrieveBy(_trainId);
    _bookingProcess.ApplyTo(train, _reservationInProgress);
    train.UpdateIn(_fleet);
  }
}