namespace KataTrainReservationTddEbook;

public interface ITrain
{
  void UpdateIn(IFleet fleet);
  void EvaluateUpFrontTrainReservability(
    uint requestedSeatCount, 
    IReservationInProgress reservationInProgress, 
    TrainHardLimitReservabilityVisitor visitor);
}

public interface IReservableTrain
{
  void EvaluateIndividualCoachSoftLimitReservability(
    uint requestedSeatCount, 
    IReservationInProgress reservationInProgress, 
    CoachSoftLimitReservabilityVisitor coachSoftLimitReservabilityVisitor);
}