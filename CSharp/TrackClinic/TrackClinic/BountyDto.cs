namespace TrackClinic;

public record BountyDto(
  Guid Id,
  string RequiredRole, //bug
  string Description,
  int CreditAmount,
  bool IsActive
);