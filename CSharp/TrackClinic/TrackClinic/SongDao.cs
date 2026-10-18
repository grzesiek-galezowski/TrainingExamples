using MongoDB.Driver;

namespace TrackClinic;

public class SongDao(MongoClient mongoClient, string databaseName)
{
  private readonly IMongoCollection<SongDetailsDto> _collection = 
    mongoClient
      .GetDatabase(databaseName)
      .GetCollection<SongDetailsDto>("Songs");

  public async Task<SongDetailsDto> ReadSong(string artistName, string songName, CancellationToken cancellationToken)
  {
    return await _collection.Find(s => s.Artist == artistName && s.Title == songName).FirstOrDefaultAsync(cancellationToken); //bug maybe
  }

  public async Task SaveSong(string artistName, CreateSongRequestDto request, CancellationToken cancellationToken)
  {
    var song = SongDetailsDto.Initial(artistName, request.Title);
    await _collection.InsertOneAsync(song, cancellationToken: cancellationToken);
  }
}