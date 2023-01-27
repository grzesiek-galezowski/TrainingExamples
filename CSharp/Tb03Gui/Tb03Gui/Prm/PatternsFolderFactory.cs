using Application.Ports;
using AtmaFileSystem;

namespace Tb03Gui.Prm;

public class PatternsFolderFactory : IPatternsFolderFactory
{
  public ITb03PatternsFolder PatternsFolder(IPatternLoadingObserver sequencer, AbsoluteDirectoryPath folderPath)
  {
    return new ActivePatternsFolder(folderPath, sequencer);
  }
}