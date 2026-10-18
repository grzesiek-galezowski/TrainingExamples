namespace TrackClinic;

/// <summary>
/// Represents an entry in the song project's timeline.
/// </summary>
/// <param name="Id">Unique identifier for the timeline entry.</param>
/// <param name="TimestampUtc">The date and time when the entry was created (UTC).</param>
/// <param name="AuthorId">The identifier of the user who created the entry.</param>
/// <param name="Title">A short title for the timeline event.</param>
/// <param name="Detail">Detailed description of the event.</param>
/// <param name="AudioSnippetUrl">Optional URL to an audio snippet associated with this event.</param>
public record TimelineEntryDto(
  Guid Id,
  DateTime TimestampUtc,
  Guid AuthorId,
  string Title,
  string Detail,
  string? AudioSnippetUrl
);