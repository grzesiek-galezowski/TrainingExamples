using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tb03Gui;

/// <summary>
/// Interaction logic for OctavePanelView.xaml
/// </summary>
public partial class OctavePanelView : UserControl
{
  private readonly Button[] _octaveButtons;
  private Tb03Octave _octave;
  private static SequenceView _observer;

  public OctavePanelView()
  {
    InitializeComponent();
    _octaveButtons = new[]
    {
      Octave1, Octave2, Octave3, Octave4, Octave5,
    };
    
    //bug 
    _octave = Tb03Octave.Octave4;
    MarkOctaveButton(Octave4);
  }

  public SequenceView Observer
  {
    get => _observer;
    set
    {
      _observer = value;
      _observer.OnOctaveChanged(_octave);
    }
  }

  private void Octave1_Click(object sender, RoutedEventArgs e)
  {
    SwitchToOctave1();
  }

  private void Octave2_Click(object sender, RoutedEventArgs e)
  {
    SwitchToOctave2();
  }

  private void Octave3_Click(object sender, RoutedEventArgs e)
  {
    SwitchToOctave3();
  }

  private void Octave4_Click(object sender, RoutedEventArgs e)
  {
    SwitchToOctave4();
  }

  private void Octave5_Click(object sender, RoutedEventArgs e)
  {
    SwitchToOctave5();
  }

  private void SwitchToOctave(Button octave1, Tb03Octave newOctave)
  {
    ResetOctaveButtons();
    MarkOctaveButton(octave1);
    Observer.OnOctaveChanged(newOctave);
  }

  public void SwitchToOctave1()
  {
    SwitchToOctave(Octave1, Tb03Octave.Octave1);
  }

  public void SwitchToOctave2()
  {
    SwitchToOctave(Octave2, Tb03Octave.Octave2);
  }

  public void SwitchToOctave3()
  {
    SwitchToOctave(Octave3, Tb03Octave.Octave3);
  }

  public void SwitchToOctave4()
  {
    SwitchToOctave(Octave4, Tb03Octave.Octave4);
  }

  public void SwitchToOctave5()
  {
    SwitchToOctave(Octave5, Tb03Octave.Octave5);
  }

  private static void MarkOctaveButton(Button button)
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