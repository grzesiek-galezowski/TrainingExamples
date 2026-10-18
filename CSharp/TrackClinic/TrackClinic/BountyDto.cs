namespace TrackClinic;

/// <summary>
/// Represents a bounty or reward offered for completing an activity.
/// </summary>
/// <param name="Id">Unique identifier for the bounty.</param>
/// <param name="RequiredRole">The role required to claim the bounty (e.g., Vocalist, Producer).</param>
/// <param name="Description">Detailed description of the bounty requirements.</param>
/// <param name="CreditAmount">The amount of credit rewarded upon completion.</param>
/// <param name="IsActive">Indicates whether the bounty is currently open for claims.</param>
public record BountyDto(
  Guid Id,
  string RequiredRole, //bug
  string Description,
  int CreditAmount,
  bool IsActive
);