namespace TrackClinic;

/// <summary>
/// Represents a significant achievement or stage in the song project.
/// </summary>
/// <param name="Id">Unique identifier for the milestone.</param>
/// <param name="Title">The title of the milestone.</param>
/// <param name="IsAchieved">Indicates whether the milestone has been completed.</param>
/// <param name="ProgressPercentage">The progress percentage specifically for this milestone.</param>
/// <param name="Activities">A list of activities that need to be completed for this milestone.</param>
public record MilestoneDto(
  Guid Id,
  string Title,
  bool IsAchieved,
  double ProgressPercentage,
  List<ActivityDto> Activities
);