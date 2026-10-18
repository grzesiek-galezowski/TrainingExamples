using System.Collections.Immutable;

using MongoDB.Bson.Serialization.Attributes;

namespace TrackClinic;

/// <summary>
/// Detailed information about a song project.
/// </summary>
/// <param name="Artist">The name of the artist.</param>
/// <param name="Title">The title of the song.</param>
/// <param name="Progress">Overall progress percentage of the song project.</param>
/// <param name="Milestones">List of milestones associated with the song.</param>
/// <param name="Timeline">Chronological list of events in the song project's history.</param>
[BsonIgnoreExtraElements]
public record SongDetailsDto(
  string Artist,
  string Title,
  int Progress,
  ImmutableList<MilestoneDto> Milestones,
  ImmutableList<TimelineEntryDto> Timeline
)
{
  public static SongDetailsDto Initial(string artistName, string songName)
  {
    return new SongDetailsDto(
      artistName,
      songName,
      0,
      ImmutableList<MilestoneDto>.Empty,
      ImmutableList<TimelineEntryDto>.Empty);
  }
}