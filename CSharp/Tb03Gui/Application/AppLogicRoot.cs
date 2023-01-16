using System.Collections.Generic;
using Application;
using Application.Ports;

namespace Tb03Gui;

public static class AppLogicRoot
{
  public static AppLogic CreateAppLogic(
    int sequencerPatternLength,
    ISequencerPositionObserver sequencerPositionObserver,
    List<IOctaveObserver> octaveObservers,
    IPatternNavigationObserver patternNavigationObserver,
    ISynthesizer synthesizer,
    ITrackPatternsObserver trackPatternsObserver,
    ITb03FolderProcessingObserver tb03FolderProcessingObserver, 
    IPatternsFolderFactory patternsFolderFactory, 
    IActiveTracksFolderFactory activeTracksFolderFactory)
  {
    var sequencer = new Sequencer(
      sequencerPatternLength,
      sequencerPositionObserver
    );
    var appLogic = new AppLogic(
      sequencer,
      new BroadcastingOctaveObserver(octaveObservers),
      sequencerPositionObserver,
      new Patterns(
        sequencer,
        patternNavigationObserver,
        synthesizer,
        patternsFolderFactory),
      new Tracks(trackPatternsObserver, activeTracksFolderFactory),
      new CheckThatFolderContainsOnlyPrmFilesStep(
        tb03FolderProcessingObserver,
        new CheckGroupsAndPatternsAndTracksCount(
          tb03FolderProcessingObserver,
          new PopulateInfoStep(tb03FolderProcessingObserver)
        )
      ),
      synthesizer);
    return appLogic;
  }
}