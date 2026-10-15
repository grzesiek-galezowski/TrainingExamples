namespace TrackClinic;

public record MilestoneDto(
  Guid Id,
  string Title,
  bool IsAchieved,
  double ProgressPercentage,
  List<ActivityDto> Activities
);