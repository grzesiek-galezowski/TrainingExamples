namespace TrackClinic;

/// <summary>
/// Request DTO for creating a new song project.
/// </summary>
/// <param name="Title">The title of the song.</param>
public record CreateSongRequestDto(string Title);