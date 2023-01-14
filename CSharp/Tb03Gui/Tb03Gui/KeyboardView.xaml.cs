using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Application.ApplicationLogic;

namespace Tb03Gui;

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
      [Key.Z] = new(C, Tb03Note.C0()),
      [Key.S] = new(CSharp, Tb03Note.CSharp0()),
      [Key.X] = new(D, Tb03Note.D0()),
      [Key.D] = new(DSharp, Tb03Note.DSharp0()),
      [Key.C] = new(E, Tb03Note.E0()),
      [Key.V] = new(F, Tb03Note.F0()),
      [Key.G] = new(FSharp, Tb03Note.FSharp0()),
      [Key.B] = new(G, Tb03Note.G0()),
      [Key.H] = new(GSharp, Tb03Note.GSharp0()),
      [Key.N] = new(A, Tb03Note.A0()),
      [Key.J] = new(ASharp, Tb03Note.ASharp0()),
      [Key.M] = new(B, Tb03Note.B0()),
    };
  }

  public bool Supports(KeyEventArgs e)
  {
    return KeyDictionary.ContainsKey(e.Key);
  }

  public void HandleKeyUp(KeyEventArgs e)
  {
    KeyDictionary[e.Key].UnMark();
  }

  public Tb03Note GetNoteFor(Key key)
  {
    return KeyDictionary[key].GetNote();
  }

  public void HandleKeyDown(KeyEventArgs e)
  {
    KeyDictionary[e.Key].Mark();
  }
}