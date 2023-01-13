﻿using System;
using System.Windows;
using System.Windows.Controls;
using Core.NullableReferenceTypesExtensions;
using MidiPlayground;
using Tb03Gui.ApplicationLogic;

namespace Tb03Gui.Track;

/// <summary>
/// Interaction logic for TrackBarView.xaml
/// </summary>
public partial class TrackBarView : UserControl
{
  private readonly AppLogic _app;

  public TrackBarView(AppLogic appLogic)
  {
    _app = appLogic;
    InitializeComponent();
  }

  public void Show()
  {
    Visibility = Visibility.Visible;
  }

  public void Hide()
  {
    Visibility = Visibility.Hidden;
  }

  public void UpdateWith(TrackEntryDto trackDtoEntry)
  {
    var patternNumber = PatternNumber.FromFlatNumber(trackDtoEntry.Pattern);
    Pattern.SelectedIndex = patternNumber.PatternNumberInGroup - 1;
    PatternGroup.SelectedIndex = patternNumber.PatternGroupNumber - 1;
    Transpose.SelectedIndex = trackDtoEntry.Transpose;
  }

  private void PlayButton_Click(object sender, RoutedEventArgs e)
  {
    try
    {
      _app.PlayPattern(
        PatternNumber.FromGroupAndNumberInGroup(
          SelectedIntFrom(PatternGroup),
          SelectedIntFrom(Pattern)),
        SelectedIntFrom(Transpose)
        ); //bug add transpose
    }
    catch (Exception ex)
    {
      MessageBox.Show(ex.ToString());
    }
  }

  private int SelectedIntFrom(ComboBox patternGroup)
  {
    return int.Parse(((ComboBoxItem)patternGroup.SelectedValue).Content.ToString().OrThrow());
  }
}