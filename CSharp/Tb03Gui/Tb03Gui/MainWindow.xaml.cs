using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Tb03Gui;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  private readonly Button[] _octaveButtons;

  public MainWindow()
  {
    InitializeComponent();
    _octaveButtons = new Button[]
    {
      Octave1, Octave2, Octave3, Octave4, Octave5,
    };
  }

  private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
  {
    if (KeyboardView.Supports(e))
    {
      KeyboardView.Handle(e);
    }
  }

  private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
  {
    if (KeyboardView.Supports(e))
    {
      KeyboardView.RestoreKeyPress(e);
      var note = KeyboardView.GetNoteFor(e.Key);
      SequenceView.HandleNote(note);
    }
    else
    {
      if (e.Key == Key.Left)
      {
        SequenceView.Back();
      }
    }
  }

  private void Octave1_Click(object sender, RoutedEventArgs e)
  {
    ResetOctaveButtons();
    MarkOctaveButton(Octave1);
    SequenceView.Octave(0);
  }

  private void Octave2_Click(object sender, RoutedEventArgs e)
  {
    ResetOctaveButtons();
    MarkOctaveButton(Octave2);
    SequenceView.Octave(1);
  }

  private void Octave3_Click(object sender, RoutedEventArgs e)
  {
    ResetOctaveButtons();
    MarkOctaveButton(Octave3);
    SequenceView.Octave(2);
  }

  private void Octave4_Click(object sender, RoutedEventArgs e)
  {
    ResetOctaveButtons();
    MarkOctaveButton(Octave4);
    SequenceView.Octave(3);
  }

  private void Octave5_Click(object sender, RoutedEventArgs e)
  {
    ResetOctaveButtons();
    MarkOctaveButton(Octave5);
    SequenceView.Octave(4);
  }

  private void MarkOctaveButton(Button button)
  {
    button.Background = new SolidColorBrush(Colors.AliceBlue);
  }

  private void ResetOctaveButtons()
  {
    foreach (var octaveButton in _octaveButtons)
    {
      octaveButton.Background = new SolidColorBrush(Colors.LightGray);
    }
  }

}