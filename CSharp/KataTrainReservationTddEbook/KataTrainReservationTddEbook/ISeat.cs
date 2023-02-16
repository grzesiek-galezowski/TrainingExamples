namespace KataTrainReservationTddEbook;

public interface ISeat
{
  bool IsFree();
  void Reserve(string bookingReference, IReservationInProgress reservationInProgress);
}

public class ReservableSeat : ISeat
{
  private bool _isFree;
  private readonly string _name;

  public ReservableSeat(bool isFree, string name)
  {
    _isFree = isFree;
    _name = name;
  }

  public bool IsFree()
  {
    return _isFree;
  }

  public void Reserve(string bookingReference, IReservationInProgress reservationInProgress)
  {
    _isFree = false;
    reservationInProgress.ReservedSeat(bookingReference, _name);
  }
}