using System.Windows;
using System.Windows.Input;

namespace Tb03Gui;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

  public MainWindow()
  {
    InitializeComponent();
    OctavePanelView.Observer = SequenceView;
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



}