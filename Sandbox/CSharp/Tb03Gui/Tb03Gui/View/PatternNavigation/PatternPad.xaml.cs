using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Application.Ports;

namespace Tb03Gui.View.PatternNavigation;

/// <summary>
/// Interaction logic for PatternGroupPad.xaml
/// </summary>
public partial class PatternPad : UserControl
{
  private int _patternNumber;

  public PatternPad()
  {
    InitializeComponent();
  }

  public IAppLogic App { get; set; }

  public int PatternNumber
  {
    get => _patternNumber;
    set
    {
      PatternButton.Content = value;
      _patternNumber = value;
    }
  }

  private async void PatternButton_Click(object sender, RoutedEventArgs e)
  {
    try
    {
      await App.PatternWasSelected(PatternNumber, new CancellationToken());
      Mark();
    }
    catch (Exception ex)
    {
      MessageBox.Show(ex.ToString());
    }
  }

  public void Mark()
  {
    PatternButton.Background = new SolidColorBrush(Colors.AliceBlue);
  }

  public void Unmark()
  {
    PatternButton.Background = new SolidColorBrush(Colors.LightGray);
  }
}