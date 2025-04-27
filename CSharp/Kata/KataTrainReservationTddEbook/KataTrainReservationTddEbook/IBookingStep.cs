namespace KataTrainReservationTddEbook;

public interface IBookingStep
{
  void Invoke(ITrain train, IReservationInProgress reservationInProgress);
}