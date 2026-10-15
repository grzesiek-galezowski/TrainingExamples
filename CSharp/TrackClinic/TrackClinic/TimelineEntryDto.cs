namespace TrackClinic;

public record TimelineEntryDto(
  Guid Id,
  DateTime TimestampUtc,
  Guid AuthorId,
  string Title,
  string Detail,
  string? AudioSnippetUrl
);