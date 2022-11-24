using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tb03Gui
{
  /// <summary>
  /// Interaction logic for KeyboardView.xaml
  /// </summary>
  public partial class KeyboardView : UserControl
  {
    private Dictionary<Key, Tb03Key> KeyDictionary { get; }

    public KeyboardView()
    {
      InitializeComponent();
      KeyDictionary = new Dictionary<Key, Tb03Key>
      {
        [Key.Z] = new(C, new Tb03Note("C")),
        [Key.S] = new(CSharp, new Tb03Note("C#")),
        [Key.X] = new(D, new Tb03Note("D")),
        [Key.D] = new(DSharp, new Tb03Note("D#")),
        [Key.C] = new(E, new Tb03Note("E")),
        [Key.V] = new(F, new Tb03Note("F")),
        [Key.G] = new(FSharp, new Tb03Note("F#")),
        [Key.B] = new(G, new Tb03Note("G")),
        [Key.H] = new(GSharp, new Tb03Note("G#")),
        [Key.N] = new(A, new Tb03Note("A")),
        [Key.J] = new(ASharp, new Tb03Note("A#")),
        [Key.M] = new(B, new Tb03Note("B")),
      };
    }

    public bool Supports(KeyEventArgs e)
    {
      return KeyDictionary.ContainsKey(e.Key);
    }

    public void Handle(KeyEventArgs e)
    {
      KeyDictionary[e.Key].Mark();
    }

    public Tb03Note GetNoteFor(Key key)
    {
      return KeyDictionary[key].GetNote();
    }

    public void RestoreKeyPress(KeyEventArgs e)
    {
      KeyDictionary[e.Key].UnMark();
    }
  }
}
