namespace TrackClinic;

public record ActivityDto(
  Guid Id,
  string Name,
  int ProgressPercentage,
  BountyDto? ActiveBounty
);