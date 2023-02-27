namespace KataTrainReservationTddEbook;

public class CoachReservabilityVisitor
{
  public void CoachCanBeReserved(ICoach value, uint requestedSeatCount, IReservationInProgress reservationInProgress)
  {
    value.Reserve(requestedSeatCount, reservationInProgress);
  }
}