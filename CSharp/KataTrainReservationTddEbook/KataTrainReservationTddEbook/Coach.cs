namespace KataTrainReservationTddEbook;

public interface ICoach
{
  bool HasPlaceFor(uint requestedSeatCount);
  bool HasPlaceWithingTheSoftLimitFor(uint requestedSeatCount);
  void Reserve(uint requestedSeatCount, IReservationInProgress reservationInProgress);
}

public class ReservableCoach : ICoach
{
  public bool HasPlaceFor(uint requestedSeatCount)
  {
    throw new System.NotImplementedException();
  }

  public bool HasPlaceWithingTheSoftLimitFor(uint requestedSeatCount)
  {
    throw new System.NotImplementedException();
  }

  public void Reserve(uint requestedSeatCount, IReservationInProgress reservationInProgress)
  {
    throw new System.NotImplementedException();
  }
}