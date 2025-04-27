using KataTrainReservationTddEbook.Request;

namespace KataTrainReservationTddEbook;

public class TicketOfficeCommandFactory : ICommandFactory
{
  public IReservationCommand CreateReservationCommand(ReservationRequestDto requestDto,
    IReservationInProgress reservationInProgress)
  {
    return new TrainReservationCommand(
      new TrainId(requestDto.TrainId), 
      new TrainDataService(), 
      reservationInProgress,
      new StandardBookingProcess(
        new CheckWhetherThereAreEnoughSeatsInTheTrain(requestDto.SeatCount,
          new CheckWhetherThereAreEnoughSeatsInAnyCoach(
            requestDto.SeatCount,
            new MakeReservation(requestDto.SeatCount)
            ))));
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