using LanguageExt;

namespace KataTrainReservationTddEbook.Response;

public record ReservationDto(string TrainId,
  Seq<TicketDto> PerSeatTickets,
  string ReservationId);