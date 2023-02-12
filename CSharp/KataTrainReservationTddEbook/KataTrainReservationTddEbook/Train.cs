namespace KataTrainReservationTddEbook;

public interface ITrain
{
  void Reserve(in uint seatCount, ISearchEngine searchEngine, IReservationInProgress reservationInProgress);
  bool HasCapacityForReservationsInAdvance();
}