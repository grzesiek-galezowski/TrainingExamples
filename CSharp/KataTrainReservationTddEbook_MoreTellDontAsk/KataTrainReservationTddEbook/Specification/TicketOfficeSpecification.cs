using KataTrainReservationTddEbook.Request;
using KataTrainReservationTddEbook.Response;

namespace KataTrainReservationTddEbook.Specification;

public class TicketOfficeSpecification
{
  [Fact]
  public void
    ShouldExecuteReservationCommandAndReturnResponseWhenMakingReservation()
  {
    //GIVEN
    var requestDto = Any.Instance<ReservationRequestDto>();
    var commandFactory = Substitute.For<ICommandFactory>();
    var reservationInProgressFactory = Substitute.For<IReservationInProgressFactory>();
    var reservationInProgress = Substitute.For<IReservationInProgress>();
    var expectedReservationDto = Any.Instance<ReservationDto>();
    var reservationCommand = Substitute.For<IReservationCommand>();

    var ticketOffice = new TicketOffice(
      reservationInProgressFactory,
      commandFactory);

    reservationInProgressFactory.FreshInstance().Returns(reservationInProgress);
    commandFactory.CreateReservationCommand(requestDto, reservationInProgress).Returns(reservationCommand);
    reservationInProgress.ToDto().Returns(expectedReservationDto);

    //WHEN
    var reservationDto = ticketOffice.MakeReservation(requestDto);

    //THEN
    Assert.Equal(expectedReservationDto, reservationDto);
    reservationCommand.Received(1).Execute();
  }

}