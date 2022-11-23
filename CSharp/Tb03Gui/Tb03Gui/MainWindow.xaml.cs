using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tb03Gui
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private Dictionary<Key, Button> _keyDictionary;
    private readonly Label[] _sequencer;
    private int _sequencerPosition;

    public MainWindow()
    {
      InitializeComponent();
      _sequencerPosition = 0;
      _sequencer = new[]
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
      _keyDictionary = new Dictionary<Key, Button>
      {
        [Key.Z] = C,
        [Key.S] = CSharp,
        [Key.X] = D,
        [Key.D] = DSharp,
        [Key.C] = E,
        [Key.V] = F,
        [Key.G] = FSharp,
        [Key.B] = G,
        [Key.H] = GSharp,
        [Key.N] = A,
        [Key.J] = ASharp,
        [Key.M] = B,
      };
      MarkCurrentSequencerPosition();
    }

    private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
    {
      if (_keyDictionary.ContainsKey(e.Key))
      {
        _keyDictionary[e.Key].Background = new SolidColorBrush(Colors.DarkGray);
      }
    }

    private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
    {
      if (_keyDictionary.ContainsKey(e.Key))
      {
        _keyDictionary[e.Key].Background = new SolidColorBrush(Colors.LightGray);
        InsertNoteIntoSequencer(e);
        ForwardSequencerPosition();
      }
      else if (e.Key == Key.Left)
      {
        UnmarkCurrentSequencerPosition();
        TryBacktrackingSequencerPosition();
        MarkCurrentSequencerPosition();
      }
    }

    private void TryBacktrackingSequencerPosition()
    {
      if (_sequencerPosition > 0)
      {
        _sequencerPosition--;
      }
    }

    private void InsertNoteIntoSequencer(KeyEventArgs e)
    {
      _sequencer[_sequencerPosition].Content = _keyDictionary[e.Key].Name;
    }

    private void ForwardSequencerPosition()
    {
      UnmarkCurrentSequencerPosition();
      TryAdvancingSequencerPosition();
      MarkCurrentSequencerPosition();
    }

    private void TryAdvancingSequencerPosition()
    {
      if (_sequencerPosition < _sequencer.Length - 1)
      {
        _sequencerPosition++;
      }
    }

    private void MarkCurrentSequencerPosition()
    {
      _sequencer[_sequencerPosition].Background = new SolidColorBrush(Colors.AliceBlue);
    }

    private void UnmarkCurrentSequencerPosition()
    {
      if (_sequencerPosition >= 0 && _sequencerPosition < _sequencer.Length)
      {
        _sequencer[_sequencerPosition].Background = new SolidColorBrush(Colors.LightGray);
      }
    }
  }
}
