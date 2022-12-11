using System.Threading.Tasks;
using AtmaFileSystem;
using MidiPlayground;

namespace Tb03Gui.ApplicationLogic;

public class AppLogic
{
  private Tb03Octave _octave = Tb03Octave.Octave4;
  private readonly Sequencer _sequencer;
  private readonly IOctaveObserver _octaveObserver;
  private readonly ISequencerPositionObserver _sequencerPositionObserver;
  private readonly Synth _synth;
  private readonly PatternNavigation _patternNavigation;

  public AppLogic(Sequencer sequencer,
    IOctaveObserver octaveObserver,
    ISequencerPositionObserver sequencerPositionObserver, 
    PatternNavigation patternNavigation)
  {
    _sequencer = sequencer;
    _octaveObserver = octaveObserver;
    _sequencerPositionObserver = sequencerPositionObserver;
    _patternNavigation = patternNavigation;
    _synth = Synth.Create();
  }

  public void SwitchToOctave(Tb03Octave newOctave)
  {
    _octave = newOctave;
    _octaveObserver.OnOctaveChanged(newOctave);
  }

  public void PreviousSequencerPosition()
  {
    _sequencer.TryBacktrackingSequencerPosition(_sequencerPositionObserver);
  }

  public void InsertNoteIntoSequencer(Tb03Note note)
  {
    _sequencer.InsertNoteIntoSequencer(note.TransposeTo(_octave));
  }

  public async Task Play()
  {
    await _sequencer.PlayOn(_synth);
  }

  public void ActivateTb03FolderPath(AbsoluteDirectoryPath folderPath)
  {
    _patternNavigation.Activate(folderPath);
  }

  public void PatternGroupWasSelected(int patternGroupNumber) //bug enum?
  {
    _patternNavigation.SelectPatternGroup(patternGroupNumber);
  }

  public void PatternWasSelected(int patternNumber)
  {
    _patternNavigation.SelectPattern(patternNumber);
  }
}