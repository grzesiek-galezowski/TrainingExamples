using NSubstitute;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Strings;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace KataTrainReservationTddEbook;

public class TrainReservationCommand : IReservationCommand
{
  private readonly string _trainId;
  private readonly uint _seatCount;
  private readonly ITrains _trains;
  private readonly ISearchEngine _searchEngine;
  private readonly IReservationInProgress _reservationInProgress;

  public TrainReservationCommand(string trainId, uint seatCount, ITrains trains, ISearchEngine searchEngine,
    IReservationInProgress reservationInProgress)
  {
    _trainId = trainId;
    _seatCount = seatCount;
    _trains = trains;
    _searchEngine = searchEngine;
    _reservationInProgress = reservationInProgress;
  }

  public void Execute()
  {
    var train = _trains.RetrieveBy(new TrainId(_trainId));
    if (train.HasCapacityForReservationsInAdvance())
    {
      train.Reserve(_seatCount, _searchEngine, _reservationInProgress);
      _trains.Update(train);
    }
    else
    {
      _reservationInProgress.NoRoomInTrainFor(_seatCount);
    }
  }
}

public class TrainReservationCommandSpecification
{
  [Fact]
  public void ShouldDowhat1()
  {
    //GIVEN
    var reservationInProgress = Any.Instance<IReservationInProgress>();
    var seatCount = Any.UnsignedInt();
    var train = Substitute.For<ITrain>();
    var trains = Substitute.For<ITrains>();
    var trainId = Any.String();
    var searchEngine = Substitute.For<ISearchEngine>();
    var command = new TrainReservationCommand(
      trainId, 
      seatCount, 
      trains, 
      searchEngine,
      reservationInProgress);

    trains.RetrieveBy(new TrainId(trainId)).Returns(train);
    train.HasCapacityForReservationsInAdvance().Returns(true);

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
  public void ShouldDowhat2()
  {
    //GIVEN
    var reservationInProgress = Substitute.For<IReservationInProgress>();
    var seatCount = Any.UnsignedInt();
    var train = Substitute.For<ITrain>();
    var trains = Substitute.For<ITrains>();
    var trainId = Any.String();
    var searchEngine = Substitute.For<ISearchEngine>();
    var command = new TrainReservationCommand(
      trainId, 
      seatCount, 
      trains, 
      searchEngine,
      reservationInProgress);

    trains.RetrieveBy(new TrainId(trainId)).Returns(train);
    train.HasCapacityForReservationsInAdvance().Returns(false);

    //WHEN
    command.Execute();

    //THEN
    reservationInProgress.Received(1).NoRoomInTrainFor(seatCount);
  }
}