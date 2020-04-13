using System;
using NSubstitute;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Strings;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace KataTrainReservationTddEbook
{
  public class TrainReservationCommand : ReservationCommand
  {
    private readonly string _trainId;
    private readonly uint _seatCount;
    private readonly Trains _trains;
    private readonly SearchEngine _searchEngine;
    private readonly ReservationInProgress _reservationInProgress;

    public TrainReservationCommand(string trainId, uint seatCount, Trains trains, SearchEngine searchEngine,
      ReservationInProgress reservationInProgress)
    {
      _trainId = trainId;
      _seatCount = seatCount;
      _trains = trains;
      _searchEngine = searchEngine;
      _reservationInProgress = reservationInProgress;
    }

    public void Execute()
    {
      var train = _trains.RetrieveBy(_trainId);
      //bug overall condition on free places
      train.Reserve(_seatCount, _searchEngine, _reservationInProgress);
      _trains.Update(train);
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
      var trains = Substitute.For<Trains>();
      var trainId = Any.String();
      var searchEngine = Substitute.For<SearchEngine>();
      var command = new TrainReservationCommand(
        trainId, 
        seatCount, 
        trains, 
        searchEngine,
        reservationInProgress);

      trains.RetrieveBy(trainId).Returns(train);
      train.MeetsReserveInAdvanceCriteriaFor(seatCount).Returns(true);

      //WHEN
      command.Execute();

      //THEN
      Received.InOrder(() =>
      {
        train.Reserve(seatCount, searchEngine, reservationInProgress);
        trains.Update(train);
      });
    }
    
    [Fact]
    public void ShouldDOWHAT2()
    {
      //GIVEN
      var reservationInProgress = Any.Instance<ReservationInProgress>();
      var seatCount = Any.UnsignedInt();
      var train = Substitute.For<Train>();
      var trains = Substitute.For<Trains>();
      var trainId = Any.String();
      var searchEngine = Substitute.For<SearchEngine>();
      var command = new TrainReservationCommand(
        trainId, 
        seatCount, 
        trains, 
        searchEngine,
        reservationInProgress);

      trains.RetrieveBy(trainId).Returns(train);
      train.MeetsReserveInAdvanceCriteriaFor(seatCount).Returns(false);

      //WHEN
      command.Execute();

      //THEN
      reservationInProgress.Received(1).NoRoomInTrainFor(seatCount);
    }


  }
}