namespace KataTrainReservationTddEbook;

public interface ITrain
{
  void UpdateIn(IFleet fleet);
  bool HasEnoughPlaceInAnyIndividualCoachFor(uint requestedSeatCount);
  bool HasTotalFreeSeatsAtLeast(uint requestedSeatCount);
  bool HasACoachThatWouldNotExceedTheSoftLimit(uint requestedSeatCount);
  void BookSeatsInTheFirstCoachThatDoesNotExceedSoftLimitAfterBooking(
    uint requestedSeatCount, 
    IReservationInProgress reservationInProgress);

  void BookSeatsInTheFirstCoachThatHasEnoughSeatsFor(
    uint requestedSeatCount, 
    IReservationInProgress reservationInProgress);
}