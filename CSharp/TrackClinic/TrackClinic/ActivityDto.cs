namespace TrackClinic;

/// <summary>
/// Represents a specific task or activity within a milestone.
/// </summary>
/// <param name="Id">Unique identifier for the activity.</param>
/// <param name="Name">The name of the activity.</param>
/// <param name="ProgressPercentage">The progress percentage for this specific activity.</param>
/// <param name="ActiveBounty">Optional active bounty associated with this activity.</param>
public record ActivityDto(
  Guid Id,
  string Name,
  int ProgressPercentage,
  BountyDto? ActiveBounty
);