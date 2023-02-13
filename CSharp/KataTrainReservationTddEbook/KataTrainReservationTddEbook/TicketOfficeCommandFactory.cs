using KataTrainReservationTddEbook.Request;

namespace KataTrainReservationTddEbook;

public class TicketOfficeCommandFactory : ICommandFactory
{
  public IReservationCommand CreateReservationCommand(ReservationRequestDto requestDto,
    IReservationInProgress reservationInProgress)
  {
    return new TrainReservationCommand(
      requestDto.TrainId,
      requestDto.SeatCount, 
      new ReferenceService(), 
      new PercentageBasedSearchEngine(), 
      reservationInProgress);
  }
}

public class TicketOfficeCommandFactorySpecification
{
  [Fact]
  public void ShouldDowhat()
  {
    //GIVEN
    var factory = new TicketOfficeCommandFactory();
    var dto = Any.Instance<ReservationRequestDto>();
    var reservationInProgress = Any.Instance<IReservationInProgress>();

    //WHEN
    var command = factory.CreateReservationCommand(dto, reservationInProgress);

    //THEN
    command.Should().BeOfType<TrainReservationCommand>();
  }
}