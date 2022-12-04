﻿using System.Collections.Generic;
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
    var sequencer = new Sequencer(mainWindow.SequenceView.SequencerPatternLength());
    var appLogic = new AppLogic(
      sequencer,
      new BroadcastingOctaveObserver(
        new List<IOctaveObserver>
        {
          mainWindow.OctavePanelView.Octave1Pad,
          mainWindow.OctavePanelView.Octave2Pad,
          mainWindow.OctavePanelView.Octave3Pad,
          mainWindow.OctavePanelView.Octave4Pad,
          mainWindow.OctavePanelView.Octave5Pad
        }),
      mainWindow.SequenceView, 
      new PatternNavigation(
        sequencer,
        mainWindow.PatternNavigationView,
        new CheckThatFolderContainsOnlyPrmFilesStep(
          mainWindow.PatternNavigationView,
          new CheckGroupsAndPatternsCount(
            mainWindow.PatternNavigationView,
            new PopulateInfoStep(
              mainWindow.PatternNavigationView)
            )
          )
        )
      );

    mainWindow.App = appLogic;
    mainWindow.SequenceView.App = appLogic;
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

    mainWindow.Show();
  }
}