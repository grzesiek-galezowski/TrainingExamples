namespace TrackClinicServiceSpecification;

public record PostBountyRequestDto(
  string RequiredRole,
  string Description,
  int Credits
);