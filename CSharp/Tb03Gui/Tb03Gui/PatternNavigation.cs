using AtmaFileSystem;

namespace Tb03Gui;

public class PatternNavigation
{
  private readonly CheckThatFolderContainsOnlyPrmFilesStep _folderProcessingChain;
  private int _patternGroupNumber = PatternNavigationConstants.InitialPatternGroup;
  private int _patternNumber = PatternNavigationConstants.InitialPattern;
  private readonly Sequencer _sequencer;
  private readonly IPatternNavigationObserver _patternNavigationObserver;
  private ITb03Folder _folder = new NoActiveFolder();

  public PatternNavigation(
    Sequencer sequencer, 
    IPatternNavigationObserver patternNavigationObserver,
    CheckThatFolderContainsOnlyPrmFilesStep chain)
  {
    _sequencer = sequencer;
    _patternNavigationObserver = patternNavigationObserver;
    _folderProcessingChain = chain;
  }

  public void Activate(AbsoluteDirectoryPath folderPath)
  {
    _folderProcessingChain.Activate(folderPath);
    _folder = new ActiveFolder(folderPath, _sequencer);
    SelectPatternGroup(_patternGroupNumber);
    SelectPattern(_patternNumber);
  }

  public void SelectPatternGroup(int patternGroupNumber)
  {
    _patternGroupNumber = patternGroupNumber;
    _patternNavigationObserver.OnPatternGroupSelectionChanged(patternGroupNumber);
    _folder.Load(_patternGroupNumber, _patternNumber);
  }

  public void SelectPattern(int patternNumber)
  {
    _patternNumber = patternNumber;
    _patternNavigationObserver.OnPatternSelectionChanged(patternNumber);
    _folder.Load(_patternGroupNumber, _patternNumber);
  }
}