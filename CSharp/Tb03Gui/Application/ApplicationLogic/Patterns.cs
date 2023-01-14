using AtmaFileSystem;

namespace Application.ApplicationLogic;

public class Patterns : IPatternNotesObserver
{
  private int _patternGroupNumber = PatternNavigationConstants.InitialPatternGroup;
  private int _patternNumberInGroup = PatternNavigationConstants.InitialPattern;
  private readonly IPatternNotesObserver _sequencer;
  private readonly IPatternNavigationObserver _patternNavigationObserver;
  private ITb03PatternsFolder _patternsFolder = new NoActivePatternsFolder();
  private readonly ISynthesizer _synthesizer;
  private readonly IPatternsFolderFactory _patternsFolderFactory;

  public Patterns(
    IPatternNotesObserver sequencer, 
    IPatternNavigationObserver patternNavigationObserver, 
    ISynthesizer synthesizer, 
    IPatternsFolderFactory patternsFolderFactory)
  {
    _sequencer = sequencer;
    _patternNavigationObserver = patternNavigationObserver;
    _synthesizer = synthesizer;
    _patternsFolderFactory = patternsFolderFactory;
  }

  public void Initialize(AbsoluteDirectoryPath folderPath)
  {
    _patternsFolder = _patternsFolderFactory.PatternsFolder(_sequencer, folderPath);
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
      .Select(p => p.Pitch).ToList();
    _synthesizer.Play(pitches);

  }
}