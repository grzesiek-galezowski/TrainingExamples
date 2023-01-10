using System.Threading.Tasks;
using AtmaFileSystem;
using MidiPlayground;

namespace Tb03Gui.ApplicationLogic;

public class Patterns : IPatternNotesObserver
{
  private int _patternGroupNumber = PatternNavigationConstants.InitialPatternGroup;
  private int _patternNumberInGroup = PatternNavigationConstants.InitialPattern;
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
    SelectPattern(_patternNumberInGroup);
  }

  public void SelectPatternGroup(int patternGroupNumber)
  {
    _patternGroupNumber = patternGroupNumber;
    _patternNavigationObserver.OnPatternGroupSelectionChanged(patternGroupNumber);
    _patternsFolder.LoadPattern(_patternGroupNumber, _patternNumberInGroup);
  }

  public void SelectPattern(int patternNumberInGroup)
  {
    _patternNumberInGroup = patternNumberInGroup;
    _patternNavigationObserver.OnPatternSelectionChanged(patternNumberInGroup);
    _patternsFolder.LoadPattern(_patternGroupNumber, _patternNumberInGroup);
  }

  public async Task PlayPatternOutOfContext(PatternNumber patternNumber)
  {
    _patternsFolder.LoadPattern(patternNumber, this);
  }

  public void PatternLoaded(SequenceDto sequence)
  {
    throw new System.NotImplementedException();
  }
}