using System;
using NSubstitute;
using TddXt.AnyRoot.Numbers;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace KataTrainReservationTddEbook
{
  public class TrainReservationCommand : ReservationCommand
  {
    private readonly uint _seatCount;
    private readonly ReservationInProgress _reservationInProgress;
    private readonly Train _train;

    public TrainReservationCommand(uint seatCount, ReservationInProgress reservationInProgress, Train train)
    {
      _seatCount = seatCount;
      _reservationInProgress = reservationInProgress;
      _train = train;
    }

    public void Execute()
    {

    }
  }

  public class TrainReservationCommandSpecification
  {
    [Fact]
    public void ShouldDOWHAT1()
    {
      //GIVEN
      var reservationInProgress = Any.Instance<ReservationInProgress>();
      var seatCount = Any.UnsignedInt();
      var train = Substitute.For<Train>();
      var command = new TrainReservationCommand(seatCount, reservationInProgress, train);

      train.HasRoomInPreferredCoachFor(seatCount).Returns(true);

      //WHEN
      command.Execute();

      //THEN
      //bug XReceived.Only()
      train.Received(1).ReserveSeatsInPreferredCoach(seatCount, reservationInProgress);
    }


  }
}