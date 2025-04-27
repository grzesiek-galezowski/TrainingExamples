using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Application.Ports;
using Core.NullableReferenceTypesExtensions;

namespace Tb03Gui.View.OctavePanel;

/// <summary>
/// Interaction logic for OctavePad.xaml
/// </summary>
public partial class OctavePad : UserControl, IOctaveObserver
{
  public IAppLogic App { get; set; }

  public OctavePad()
  {
    InitializeComponent();
  }

  public string Text
  {
    get => OctaveButton.Content.ToString().OrThrow();
    set => OctaveButton.Content = value;
  }

  public Tb03Octave AssignedOctave { get; set; }

  private void Octave1_Click(object sender, RoutedEventArgs e)
  {
    App.SwitchToOctave(AssignedOctave);
    MarkOctaveButton(OctaveButton);
  }

  private static void MarkOctaveButton(Control octaveButton)
  {
    octaveButton.Background = new SolidColorBrush(Colors.AliceBlue);
  }

  private static void UnMarkOctaveButton(Control octaveButton)
  {
    octaveButton.Background = new SolidColorBrush(Colors.LightGray);
  }

  public void OnOctaveChanged(Tb03Octave newOctave)
  {
    if (newOctave == AssignedOctave)
    {
      MarkOctaveButton(OctaveButton);
    }
    else
    {
      UnMarkOctaveButton(OctaveButton);
    }
  }
}