using Application.Ports;
using AtmaFileSystem;

namespace Application;

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

  public async Task Initialize(AbsoluteDirectoryPath folderPath, CancellationToken cancellationToken)
  {
    _patternsFolder = _patternsFolderFactory.PatternsFolder(_sequencer, folderPath);
    await SelectPatternGroup(_patternGroupNumber, cancellationToken);
    await SelectPattern(_patternNumberInGroup, cancellationToken);
  }

  public async Task SelectPatternGroup(int patternGroupNumber, CancellationToken cancellationToken)
  {
    _patternGroupNumber = patternGroupNumber;
    _patternNavigationObserver.OnPatternGroupSelectionChanged(patternGroupNumber);
    await _patternsFolder.LoadPattern(_patternGroupNumber, _patternNumberInGroup, cancellationToken);
  }

  public async Task SelectPattern(int patternNumberInGroup, CancellationToken cancellationToken)
  {
    _patternNumberInGroup = patternNumberInGroup;
    _patternNavigationObserver.OnPatternSelectionChanged(patternNumberInGroup);
    await _patternsFolder.LoadPattern(_patternGroupNumber, _patternNumberInGroup, cancellationToken);
  }

  public async Task PlayPatternOutOfContext(PatternNumber patternNumber, int transpose,
    CancellationToken cancellationToken)
  {
    await _patternsFolder.LoadPattern(patternNumber, transpose, this, cancellationToken);
  }

  public async Task PatternLoaded(SequenceDto sequence, CancellationToken cancellationToken)
  {
    var pitches = Tb03Note.NotesFrom(sequence.Steps)
      .Select(p => p.Pitch).ToList();
    await _synthesizer.Play(pitches, cancellationToken);

  }

  public void SaveCurrentPatternFrom(Sequencer sequencer)
  {
    sequencer.SavePattern(_patternsFolder, PatternNumber.FromGroupAndNumberInGroup(_patternGroupNumber, _patternNumberInGroup));
  }
}