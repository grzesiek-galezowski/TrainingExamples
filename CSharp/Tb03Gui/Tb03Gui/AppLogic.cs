using MidiPlayground;
using System.Threading.Tasks;
using AtmaFileSystem;

namespace Tb03Gui;

public class AppLogic
{
  private Tb03Octave _octave = Tb03Octave.Octave4;
  private readonly Sequencer _sequencer;
  private readonly IOctaveObserver _octaveObserver;
  private readonly ISequencerPositionObserver _sequencerPositionObserver;
  private readonly Synth _synth;
  private readonly CheckThatFolderContainsOnlyPrmFilesStep _folderProcessingChain;

  public AppLogic(Sequencer sequencer,
    IOctaveObserver octaveObserver,
    ISequencerPositionObserver sequencerPositionObserver, 
    ITb03FolderProcessingObserver folderProcessingObserver)
  {
    _sequencer = sequencer;
    _octaveObserver = octaveObserver;
    _sequencerPositionObserver = sequencerPositionObserver;
    _synth = Synth.Create();
    _folderProcessingChain =
      new CheckThatFolderContainsOnlyPrmFilesStep(
        folderProcessingObserver,
        new CheckGroupsAndPatternsCount(
          folderProcessingObserver,
          new PopulateInfoStep(
            folderProcessingObserver)));
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
    _sequencer.InsertNoteIntoSequencer(note, _sequencerPositionObserver, _octave);
  }

  public async Task Play()
  {
    await _sequencer.PlayOn(_synth);
  }

  public void HandleTb03FolderPath(AbsoluteDirectoryPath folderPath, ITb03FolderProcessingObserver observer)
  {
    _folderProcessingChain
      .Handle(folderPath);
  }
}