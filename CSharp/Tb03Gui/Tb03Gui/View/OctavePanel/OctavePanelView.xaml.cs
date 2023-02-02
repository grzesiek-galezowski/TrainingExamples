using System.Windows.Controls;
using Application.Ports;

namespace Tb03Gui.View.OctavePanel;

/// <summary>
/// Interaction logic for OctavePanelView.xaml
/// </summary>
public partial class OctavePanelView : UserControl
{
  private IAppLogic _app;

  public OctavePanelView()
  {
    InitializeComponent();
  }

  public IAppLogic App
  {
    get => _app;
    set
    {
      _app = value;
      _app.SwitchToOctave(Tb03Octave.Octave4);
    }
  }
}