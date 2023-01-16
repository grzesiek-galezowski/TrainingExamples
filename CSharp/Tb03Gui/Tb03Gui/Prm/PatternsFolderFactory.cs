using Application.Ports;
using AtmaFileSystem;

namespace Tb03Gui.Prm;

public class PatternsFolderFactory : IPatternsFolderFactory
{
  public ITb03PatternsFolder PatternsFolder(IPatternNotesObserver sequencer, AbsoluteDirectoryPath folderPath)
  {
    return new ActivePatternsFolder(folderPath, sequencer);
  }
}