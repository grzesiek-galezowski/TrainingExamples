namespace KataTrainReservationTddEbook
{
  public interface Train
  {
    void Reserve(in uint seatCount, SearchEngine searchEngine, ReservationInProgress reservationInProgress);
    bool MeetsReserveInAdvanceCriteriaFor(in uint seatCount);
  }
}