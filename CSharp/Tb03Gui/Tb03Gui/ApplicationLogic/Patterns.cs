using AtmaFileSystem;

namespace Tb03Gui.ApplicationLogic;

public class Patterns
{
  private int _patternGroupNumber = PatternNavigationConstants.InitialPatternGroup;
  private int _patternNumber = PatternNavigationConstants.InitialPattern;
  private readonly Sequencer _sequencer;
  private readonly IPatternNavigationObserver _patternNavigationObserver;
  private ITb03PatternsFolder _patternsFolder = new NoActivePatternsFolder();

  public Patterns(
    Sequencer sequencer, 
    IPatternNavigationObserver patternNavigationObserver)
  {
    _sequencer = sequencer;
    _patternNavigationObserver = patternNavigationObserver;
  }

  public void Initialize(AbsoluteDirectoryPath folderPath)
  {
    _patternsFolder = new ActivePatternsFolder(folderPath, _sequencer);
    SelectPatternGroup(_patternGroupNumber);
    SelectPattern(_patternNumber);
  }

  public void SelectPatternGroup(int patternGroupNumber)
  {
    _patternGroupNumber = patternGroupNumber;
    _patternNavigationObserver.OnPatternGroupSelectionChanged(patternGroupNumber);
    _patternsFolder.LoadPattern(_patternGroupNumber, _patternNumber);
  }

  public void SelectPattern(int patternNumber)
  {
    _patternNumber = patternNumber;
    _patternNavigationObserver.OnPatternSelectionChanged(patternNumber);
    _patternsFolder.LoadPattern(_patternGroupNumber, _patternNumber);
  }
}