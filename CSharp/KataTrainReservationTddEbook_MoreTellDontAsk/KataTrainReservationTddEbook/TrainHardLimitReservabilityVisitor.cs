namespace KataTrainReservationTddEbook;

public class TrainHardLimitReservabilityVisitor
{
  private readonly CoachSoftLimitReservabilityVisitor _coachSoftLimitReservabilityVisitor;

  public TrainHardLimitReservabilityVisitor(CoachSoftLimitReservabilityVisitor coachSoftLimitReservabilityVisitor)
  {
    _coachSoftLimitReservabilityVisitor = coachSoftLimitReservabilityVisitor;
  }

  public void TrainIsReservable(IReservableTrain train, uint requestedSeatCount, IReservationInProgress reservationInProgress)
  {
    train.EvaluateIndividualCoachSoftLimitReservability(
      requestedSeatCount, 
      reservationInProgress, 
      _coachSoftLimitReservabilityVisitor);
    
    //bug but when should I save the train?
    //bug train.UpdateIn(_fleet);
  }

  public void TrainIsNotReservable(uint requestedSeatCount, IReservationInProgress reservationInProgress)
  {
    reservationInProgress.NoRoomInTrainFor(requestedSeatCount);
  }
}