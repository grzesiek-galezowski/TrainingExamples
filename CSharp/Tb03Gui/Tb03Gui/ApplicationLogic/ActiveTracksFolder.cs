using System.IO;
using AtmaFileSystem;
using MidiPlayground;

namespace Tb03Gui.ApplicationLogic;

public class ActiveTracksFolder : ITb03TracksFolder
{
  private readonly AbsoluteDirectoryPath _folderPath;
  private readonly ITrackPatternsObserver _trackPatternsObserver;

  public ActiveTracksFolder(
    AbsoluteDirectoryPath folderPath,
    ITrackPatternsObserver trackPatternsObserver)
  {
    _folderPath = folderPath;
    _trackPatternsObserver = trackPatternsObserver;
  }

  public void LoadTrack(int trackNumber)
  {
    var fileName = Tb03TrackFileName.For(_folderPath, trackNumber);
    var fileContent = File.ReadAllText(fileName.ToString());
    var trackDto = PrmParser.ParseIntoTrack(fileContent);
    _trackPatternsObserver.TrackLoaded(trackDto);
  }
}