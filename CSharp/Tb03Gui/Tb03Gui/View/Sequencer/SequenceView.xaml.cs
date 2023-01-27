using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Application.Ports;
using AtmaFileSystem;

namespace Tb03Gui.View.Sequencer;

/// <summary>
/// Interaction logic for SequenceView.xaml
/// </summary>
public partial class SequenceView : UserControl, ISequencerPositionObserver, IPatternSavingObserver
{
  private readonly SequencerPad[] _sequencerPads;

  public SequenceView()
  {
    InitializeComponent();
    _sequencerPads = new[]
    {
      P1,
      P2,
      P3,
      P4,
      P5,
      P6,
      P7,
      P8,
      P9,
      P10,
      P11,
      P12,
      P13,
      P14,
      P15,
      P16,
    };
    _sequencerPads[Application.Sequencer.InitialSequencerPosition].Mark();
  }

  public IAppLogic App { get; set; }

  private async void PlayPause_Click(object sender, RoutedEventArgs e)
  {
    try
    {
      await App.PlayCurrentPattern(new CancellationToken());
    }
    catch (Exception exception)
    {
      MessageBox.Show(exception.ToString());
    }
  }

  private void SaveButton_OnClick(object sender, RoutedEventArgs e)
  {
    App.SaveCurrentPattern(this);
  }


  public void OnSequencerPositionChange(int prevPosition, int newPosition)
  {
    _sequencerPads[prevPosition].Unmark();
    _sequencerPads[newPosition].Mark();
  }

  public void OnNoteInsert(int sequencerPosition, Tb03Note latestNote)
  {
    _sequencerPads[sequencerPosition].SetNote(latestNote);
  }

  public int SequencerPatternLength()
  {
    return _sequencerPads.Length;
  }

  public void PatternSaved(AbsoluteFilePath filePath)
  {
    MessageBox.Show($"Pattern saved at {filePath}");
  }
}
