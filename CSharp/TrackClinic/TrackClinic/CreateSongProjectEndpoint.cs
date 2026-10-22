using Serilog;

namespace TrackClinic;

public class CreateSongProjectEndpoint(string artistName, CreateSongRequestDto request, SongDao songDao)
{
  public async Task Handle(CancellationToken cancellationToken)
  {
    await songDao.SaveSong(artistName, request, cancellationToken);
    Log.Information("Created song project for {ArtistName}: {SongTitle}", artistName, request.Title);
  }
}