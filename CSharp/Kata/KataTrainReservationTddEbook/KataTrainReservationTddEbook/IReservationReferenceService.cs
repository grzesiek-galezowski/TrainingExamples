namespace KataTrainReservationTddEbook;

public interface IReservationReferenceService
{
  string GetBookingReference();
}

public class ReservationReferenceService : IReservationReferenceService
{
  public string GetBookingReference()
  {
    throw new System.NotImplementedException();
  }
}