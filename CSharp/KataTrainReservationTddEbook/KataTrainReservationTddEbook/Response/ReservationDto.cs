using System.Collections.Generic;

namespace KataTrainReservationTddEbook.Response;

public class ReservationDto
{
  public readonly string TrainId;
  public readonly string ReservationId;
  public readonly List<TicketDto> PerSeatTickets;

  public ReservationDto(
    string trainId,
    List<TicketDto> perSeatTickets,
    string reservationId)
  {
    TrainId = trainId;
    PerSeatTickets = perSeatTickets;
    ReservationId = reservationId;
  }
}