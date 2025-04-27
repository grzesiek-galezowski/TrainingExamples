namespace KataTrainReservationTddEbook;

public interface IBookingProcess
{
  void ApplyTo(ITrain train, IReservationInProgress reservationInProgress);
}