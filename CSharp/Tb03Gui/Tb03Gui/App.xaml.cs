using System.Collections.Generic;
using System.Windows;
using Application.Ports;
using Tb03Gui.Midi;
using Tb03Gui.Prm;
using Tb03Gui.View;

namespace Tb03Gui;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
  private Synthesizer _synthesizer = null!;

  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    _synthesizer = Synthesizer.Create();
    var mainWindow = new MainWindow(Synthesizer.GetMidiDevices(), _synthesizer.CurrentMidiDevice());
    var appLogic = AppLogicRoot.CreateAppLogic(
      mainWindow.SequenceView.SequencerPatternLength(),
      mainWindow.SequenceView,
      new List<IOctaveObserver>
      {
        mainWindow.OctavePanelView.Octave1Pad,
        mainWindow.OctavePanelView.Octave2Pad,
        mainWindow.OctavePanelView.Octave3Pad,
        mainWindow.OctavePanelView.Octave4Pad,
        mainWindow.OctavePanelView.Octave5Pad
      },
      mainWindow.PatternNavigationView,
      _synthesizer,
      mainWindow.TrackNavigationView,
      mainWindow.FolderManagement, new PatternsFolderFactory(), new ActiveTracksFolderFactory());

    mainWindow.App = appLogic;
    mainWindow.TrackNavigationView.Initialize(appLogic);
    mainWindow.FolderManagement.App = appLogic;
    mainWindow.SequenceView.App = appLogic;
    mainWindow.SequenceView.P1.App = appLogic;
    mainWindow.SequenceView.P2.App = appLogic;
    mainWindow.SequenceView.P3.App = appLogic;
    mainWindow.SequenceView.P4.App = appLogic;
    mainWindow.SequenceView.P5.App = appLogic;
    mainWindow.SequenceView.P6.App = appLogic;
    mainWindow.SequenceView.P7.App = appLogic;
    mainWindow.SequenceView.P8.App = appLogic;
    mainWindow.SequenceView.P9.App = appLogic;
    mainWindow.SequenceView.P10.App = appLogic;
    mainWindow.SequenceView.P11.App = appLogic;
    mainWindow.SequenceView.P12.App = appLogic;
    mainWindow.SequenceView.P13.App = appLogic;
    mainWindow.SequenceView.P14.App = appLogic;
    mainWindow.SequenceView.P15.App = appLogic;
    mainWindow.SequenceView.P16.App = appLogic;
    mainWindow.OctavePanelView.App = appLogic;
    mainWindow.OctavePanelView.Octave1Pad.App = appLogic;
    mainWindow.OctavePanelView.Octave2Pad.App = appLogic;
    mainWindow.OctavePanelView.Octave3Pad.App = appLogic;
    mainWindow.OctavePanelView.Octave4Pad.App = appLogic;
    mainWindow.OctavePanelView.Octave5Pad.App = appLogic;
    mainWindow.PatternNavigationView.App = appLogic;
    
    mainWindow.PatternNavigationView.Group1Pad.App = appLogic;
    mainWindow.PatternNavigationView.Group2Pad.App = appLogic;
    mainWindow.PatternNavigationView.Group3Pad.App = appLogic;
    mainWindow.PatternNavigationView.Group4Pad.App = appLogic;

    mainWindow.PatternNavigationView.Pattern1Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern2Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern3Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern4Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern5Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern6Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern7Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern8Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern9Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern10Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern11Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern12Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern13Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern14Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern15Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern16Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern17Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern18Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern19Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern20Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern21Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern22Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern23Pad.App = appLogic;
    mainWindow.PatternNavigationView.Pattern24Pad.App = appLogic;

    mainWindow.TrackNavigationView.Track1Pad.App = appLogic;
    mainWindow.TrackNavigationView.Track2Pad.App = appLogic;
    mainWindow.TrackNavigationView.Track3Pad.App = appLogic;
    mainWindow.TrackNavigationView.Track4Pad.App = appLogic;
    mainWindow.TrackNavigationView.Track5Pad.App = appLogic;
    mainWindow.TrackNavigationView.Track6Pad.App = appLogic;
    mainWindow.TrackNavigationView.Track7Pad.App = appLogic;

    mainWindow.Show();
  }

  protected override void OnExit(ExitEventArgs e)
  {
    base.OnExit(e);
    _synthesizer.Dispose();
  }
}