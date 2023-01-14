using Application.ApplicationLogic;
using AtmaFileSystem;

namespace Tb03Gui.Midi;

public class PatternsFolderFactory : IPatternsFolderFactory
{
  public ITb03PatternsFolder PatternsFolder(IPatternNotesObserver sequencer, AbsoluteDirectoryPath folderPath)
  {
    return new ActivePatternsFolder(folderPath, sequencer);
  }
}