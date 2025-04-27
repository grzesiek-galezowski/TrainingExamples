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
public partial class PatternGroupPad : UserControl
{
  private int _patternGroupNumber;

  public PatternGroupPad()
  {
    InitializeComponent();
  }

  public IAppLogic App { get; set; }

  public int PatternGroupNumber
  {
    get => _patternGroupNumber;
    set
    {
      GroupButton.Content = value;
      _patternGroupNumber = value;
    }
  }

  private async void GroupButton_Click(object sender, RoutedEventArgs e)
  {
    try
    {
      await App.PatternGroupWasSelected(PatternGroupNumber, new CancellationToken());
    }
    catch (Exception ex)
    {
      MessageBox.Show(ex.ToString());
    }
  }

  public void Mark()
  {
    GroupButton.Background = new SolidColorBrush(Colors.AliceBlue);
  }

  public void Unmark()
  {
    GroupButton.Background = new SolidColorBrush(Colors.LightGray);
  }
}