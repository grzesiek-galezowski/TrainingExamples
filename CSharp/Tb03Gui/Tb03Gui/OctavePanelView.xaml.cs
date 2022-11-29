using System.Windows.Controls;

namespace Tb03Gui;

/// <summary>
/// Interaction logic for OctavePanelView.xaml
/// </summary>
public partial class OctavePanelView : UserControl
{
  private AppLogic _app;

  public OctavePanelView()
  {
    InitializeComponent();
  }

  public AppLogic App
  {
    get => _app;
    set
    {
      _app = value;
      _app.SwitchToOctave(Tb03Octave.Octave4);
    }
  }
}