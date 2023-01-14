using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Application.ApplicationLogic;
using Core.NullableReferenceTypesExtensions;

namespace Tb03Gui.Sequencer;

/// <summary>
/// Interaction logic for SequencerPad.xaml
/// </summary>
public partial class SequencerPad : UserControl, IParameterToggleObserver
{
  private AppLogic? _app;

  public SequencerPad()
  {
    InitializeComponent();
  }

  public AppLogic App
  {
    get => _app.OrThrow();
    set => _app = value;
  }

  public int Number { get; set; }

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
    SetAccent(latestNote.Accent);
    SetSlide(latestNote.Slide);
    StateButton.Content = latestNote.State;
  }

  private void SetSlide(bool latestNoteSlide)
  {
    SlideButton.Background =
      latestNoteSlide ? new SolidColorBrush(Colors.PaleGreen) : new SolidColorBrush(Colors.LightGray);
  }

  private void SetAccent(bool newAccentValue)
  {
    AccentButton.Background =
      newAccentValue ? new SolidColorBrush(Colors.PaleGreen) : new SolidColorBrush(Colors.LightGray);
  }

  private void AccentButton_Click(object sender, RoutedEventArgs e)
  {
    App.ToggleSequencerNoteAccent(Number, this);
  }

  public void OnAccentChanged(bool newAccentValue)
  {
    SetAccent(newAccentValue);
  }

  public void OnSlideChanged(bool newSlideValue)
  {
    SetSlide(newSlideValue);
  }

  private void SlideButton_Click(object sender, RoutedEventArgs e)
  {
    App.ToggleNoteSlide(Number, (IParameterToggleObserver)this);
  }
}
