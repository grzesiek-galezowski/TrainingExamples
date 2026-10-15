using System.Collections.Immutable;

using MongoDB.Bson.Serialization.Attributes;

namespace TrackClinic;

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