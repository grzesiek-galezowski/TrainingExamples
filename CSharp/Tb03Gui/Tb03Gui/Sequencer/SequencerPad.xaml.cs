using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Tb03Gui.ApplicationLogic;

namespace Tb03Gui.Sequencer;

/// <summary>
/// Interaction logic for SequencerPad.xaml
/// </summary>
public partial class SequencerPad : UserControl
{
  public SequencerPad()
  {
    InitializeComponent();
  }

  public AppLogic App { get; set; }

  public void Mark()
  {
    PadLabel.Background = new SolidColorBrush(Colors.AliceBlue);
  }

  public void Unmark()
  {
    PadLabel.Background = new SolidColorBrush(Colors.LightGray);
  }

  public void SetNote(Tb03Note latestNote)
  {
    PadLabel.Content = latestNote;
    AccentButton.Background = latestNote.Accent ? new SolidColorBrush(Colors.PaleGreen) : new SolidColorBrush(Colors.LightGray);
    SlideButton.Background = latestNote.Slide ? new SolidColorBrush(Colors.PaleGreen) : new SolidColorBrush(Colors.LightGray);
    StateButton.Content = latestNote.State;
  }

  private void AccentButton_Click(object sender, RoutedEventArgs e)
  {
    //bug trigger specific note accent
    //bug situation where there isn't a note
    throw new Exception("");
    App.ToggleSequencerNoteAccent();
  }
}