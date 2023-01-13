using System.Linq;
using System.Threading.Tasks;
using AtmaFileSystem;
using Midi.Enums;
using MidiPlayground;

namespace Tb03Gui.ApplicationLogic;

public class Patterns : IPatternNotesObserver
{
  private int _patternGroupNumber = PatternNavigationConstants.InitialPatternGroup;
  private int _patternNumberInGroup = PatternNavigationConstants.InitialPattern;
  private readonly Sequencer _sequencer;
  private readonly IPatternNavigationObserver _patternNavigationObserver;
  private ITb03PatternsFolder _patternsFolder = new NoActivePatternsFolder();
  private readonly Synthesizer _synthesizer;

  public Patterns(
    Sequencer sequencer, 
    IPatternNavigationObserver patternNavigationObserver, 
    Synthesizer synthesizer)
  {
    _sequencer = sequencer;
    _patternNavigationObserver = patternNavigationObserver;
    _synthesizer = synthesizer;
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

  public void PlayPatternOutOfContext(PatternNumber patternNumber, int transpose)
  {
    _patternsFolder.LoadPattern(patternNumber, transpose, this);
  }

  public void PatternLoaded(SequenceDto sequence)
  {
    var pitches = Tb03Note.NotesFrom(sequence.Steps)
      .Select(p => (Pitch)p.Pitch).ToList();
    _synthesizer.Play(pitches);

  }
}