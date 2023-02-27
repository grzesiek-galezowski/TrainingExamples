namespace KataTrainReservationTddEbook;

public class CoachSoftLimitReservabilityVisitor
{
  public void CoachNotFound(uint requestedSeatCount, IReservationInProgress reservationInProgress, ReservableTrain train)
  {
    train.EvaluateIndividualCoachReservability(
      requestedSeatCount, 
      reservationInProgress, 
      new CoachReservabilityVisitor());
  }

  public void CoachFound(ICoach coach, uint requestedSeatCount, IReservationInProgress reservationInProgress)
  {
    coach.Reserve(requestedSeatCount, reservationInProgress);
  }
}