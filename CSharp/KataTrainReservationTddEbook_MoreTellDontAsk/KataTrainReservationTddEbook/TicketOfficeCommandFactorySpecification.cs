using KataTrainReservationTddEbook.Request;

namespace KataTrainReservationTddEbook;

public class TicketOfficeCommandFactorySpecification
{
  [Fact]
  public void ShouldDowhat()
  {
    //GIVEN
    var factory = new TicketOfficeCommandFactory(
      new TrainDataService(),
      new TrainHardLimitReservabilityVisitor(
        new CoachSoftLimitReservabilityVisitor()));
    var dto = Any.Instance<ReservationRequestDto>();
    var reservationInProgress = Any.Instance<IReservationInProgress>();

    //WHEN
    var command = factory.CreateReservationCommand(dto, reservationInProgress);

    //THEN
    command.Should().BeOfType<TrainReservationCommand>();
  }
}