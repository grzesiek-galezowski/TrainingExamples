namespace KataTrainReservationTddEbook;

public class StandardBookingProcess : IBookingProcess
{
  private readonly IBookingStep _next;

  public StandardBookingProcess(IBookingStep next)
  {
    _next = next;
  }

  public void ApplyTo(ITrain train, IReservationInProgress reservationInProgress)
  {
    _next.Invoke(train, reservationInProgress);
  }
}