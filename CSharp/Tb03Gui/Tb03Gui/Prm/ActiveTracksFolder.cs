using System.IO;
using Application;
using Application.Ports;
using AtmaFileSystem;

namespace Tb03Gui.Prm;

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

  public void LoadTrack(int trackNumber, ITrackPatternsObserver adHocObserver)
  {
    var fileName = Tb03TrackFileName.For(_folderPath, trackNumber);
    var fileContent = File.ReadAllText(fileName.ToString());
    var trackDto = PrmParser.ParseIntoTrack(fileContent);
    _trackPatternsObserver.TrackLoaded(trackDto);
    adHocObserver.TrackLoaded(trackDto);
  }
}