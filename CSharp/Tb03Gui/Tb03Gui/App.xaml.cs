using System.Collections.Generic;
using System.Windows;

namespace Tb03Gui;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    var mainWindow = new MainWindow();
    var sequencerModel = new Sequencer(mainWindow.SequenceView.SequencerPatternLength());
    var appLogic = new AppLogic(
      sequencerModel,
      new BroadcastingOctaveObserver(
        new List<IOctaveObserver>
        {
          mainWindow.OctavePanelView.Octave1Pad,
          mainWindow.OctavePanelView.Octave2Pad,
          mainWindow.OctavePanelView.Octave3Pad,
          mainWindow.OctavePanelView.Octave4Pad,
          mainWindow.OctavePanelView.Octave5Pad
        }),
      mainWindow.SequenceView
    );

    mainWindow.App = appLogic;
    mainWindow.SequenceView.App = appLogic;
    mainWindow.OctavePanelView.App = appLogic;
    mainWindow.OctavePanelView.Octave1Pad.App = appLogic;
    mainWindow.OctavePanelView.Octave2Pad.App = appLogic;
    mainWindow.OctavePanelView.Octave3Pad.App = appLogic;
    mainWindow.OctavePanelView.Octave4Pad.App = appLogic;
    mainWindow.OctavePanelView.Octave5Pad.App = appLogic;
    mainWindow.Show();
  }
}