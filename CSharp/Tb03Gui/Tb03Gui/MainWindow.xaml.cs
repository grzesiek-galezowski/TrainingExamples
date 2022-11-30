using System.Windows;
using System.Windows.Input;
using AtmaFileSystem;
using Midi.Messages;
using Ookii.Dialogs.Wpf;

namespace Tb03Gui;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
  }

  public AppLogic App { get; set; }

  private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
  {
    if (KeyboardView.Supports(e))
    {
      KeyboardView.HandleKeyDown(e);
    }
  }

  private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
  {
    if (KeyboardView.Supports(e))
    {
      var note = KeyboardView.GetNoteFor(e.Key);
      App.InsertNoteIntoSequencer(note);
      KeyboardView.HandleKeyUp(e);
    }
    else if (e.Key == Key.Left)
    {
      App.PreviousSequencerPosition();
    }
    else if (e.Key == Key.D1)
    {
      App.SwitchToOctave(Tb03Octave.Octave1);
    }
    else if (e.Key == Key.D2)
    {
      App.SwitchToOctave(Tb03Octave.Octave2);
    }
    else if (e.Key == Key.D3)
    {
      App.SwitchToOctave(Tb03Octave.Octave3);
    }
    else if (e.Key == Key.D4)
    {
      App.SwitchToOctave(Tb03Octave.Octave4);
    }
    else if (e.Key == Key.D5)
    {
      App.SwitchToOctave(Tb03Octave.Octave5);
    }
  }

  private void Button_Click(object sender, RoutedEventArgs e)
  {
    var openFolderDialog = new VistaFolderBrowserDialog();
    if (openFolderDialog.ShowDialog().GetValueOrDefault())
    {
      var folderPath = AbsoluteDirectoryPath.Value(openFolderDialog.SelectedPath);
      new CheckThatFolderContainsOnlyPrmFilesStep(
          new CheckGroupsAndPatternsCount(
            new PopulateInfoStep(SelectTb03BackupFolderButton)))
        .Handle(folderPath);
    }
  }
}