using MutaSynthEngine;

namespace MutaSynth;

public partial class MainPage : ContentPage
{
    private enum SynthModule
    {
        Keyboard,
        Voice,
        Osc1,
        Osc2,
        Osc3,
        Mixer,
        Filter,
        Amplifier,
        Output
    }

    private readonly IAudioDriver _driver;
    private bool _isLoaded;
    private SynthModule _selectedModule = SynthModule.Keyboard;

    public MainPage(IAudioDriver driver)
    {
        InitializeComponent();
        _driver = driver;
        RefreshSelectedModule();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_isLoaded)
        {
            return;
        }

        await _driver.InitializeAsync();
        LoadSelections();
        StatusLabel.Text = "Ready — select devices and play a MIDI note";
        _isLoaded = true;
    }

    private void OnKeyboardModuleTapped(object? sender, TappedEventArgs e)
    {
        _selectedModule = SynthModule.Keyboard;
        RefreshSelectedModule();
    }

    private void OnVoiceModuleTapped(object? sender, TappedEventArgs e)
    {
        _selectedModule = SynthModule.Voice;
        RefreshSelectedModule();
    }

    private void OnOsc1ModuleTapped(object? sender, TappedEventArgs e)
    {
        _selectedModule = SynthModule.Osc1;
        RefreshSelectedModule();
    }

    private void OnOsc2ModuleTapped(object? sender, TappedEventArgs e)
    {
        _selectedModule = SynthModule.Osc2;
        RefreshSelectedModule();
    }

    private void OnOsc3ModuleTapped(object? sender, TappedEventArgs e)
    {
        _selectedModule = SynthModule.Osc3;
        RefreshSelectedModule();
    }

    private void OnMixerModuleTapped(object? sender, TappedEventArgs e)
    {
        _selectedModule = SynthModule.Mixer;
        RefreshSelectedModule();
    }

    private void OnFilterModuleTapped(object? sender, TappedEventArgs e)
    {
        _selectedModule = SynthModule.Filter;
        RefreshSelectedModule();
    }

    private void OnAmplifierModuleTapped(object? sender, TappedEventArgs e)
    {
        _selectedModule = SynthModule.Amplifier;
        RefreshSelectedModule();
    }

    private void OnOutputModuleTapped(object? sender, TappedEventArgs e)
    {
        _selectedModule = SynthModule.Output;
        RefreshSelectedModule();
    }

    private void OnVolumeChanged(object? sender, ValueChangedEventArgs e)
    {
        _driver.Volume = (float)e.NewValue;
    }

    private void OnMidiInputChanged(object? sender, EventArgs e)
    {
        if (MidiInputPicker.SelectedItem is AudioDriverDeviceOption option)
        {
            _driver.SelectMidiInputDevice(option.Id);
            StatusLabel.Text = $"MIDI input: {option.Name}";
        }
    }

    private void OnMidiChannelChanged(object? sender, EventArgs e)
    {
        if (MidiChannelPicker.SelectedItem is int channel)
        {
            _driver.SelectMidiChannel(channel);
            StatusLabel.Text = $"MIDI channel: {channel}";
        }
    }

    private void OnNotePlaybackModeChanged(object? sender, EventArgs e)
    {
        if (NotePlaybackModePicker.SelectedItem is NotePlaybackMode notePlaybackMode)
        {
            _driver.SelectNotePlaybackMode(notePlaybackMode);
            StatusLabel.Text = $"Playback mode: {notePlaybackMode}";
        }
    }

    private void OnSemiChanged(object? sender, ValueChangedEventArgs e)
    {
        var semiOffset = (int)Math.Round(e.NewValue);
        _driver.SelectSemiOffset(semiOffset);
        SemiValueLabel.Text = $"{semiOffset:+#;-#;0} semitones";
        StatusLabel.Text = $"Oscillator semi: {semiOffset:+#;-#;0}";
    }

    private void OnWaveformChanged(object? sender, EventArgs e)
    {
        if (WaveformPicker.SelectedItem is OscillatorWaveform waveform)
        {
            _driver.SelectWaveform(waveform);
            StatusLabel.Text = $"Oscillator wave: {waveform}";
        }
    }

    private void OnCentsChanged(object? sender, ValueChangedEventArgs e)
    {
        var centsOffset = (int)Math.Round(e.NewValue);
        _driver.SelectCentsOffset(centsOffset);
        CentsValueLabel.Text = $"{centsOffset:+#;-#;0} cents";
        StatusLabel.Text = $"Oscillator cents: {centsOffset:+#;-#;0}";
    }

    private void OnOsc2SemiChanged(object? sender, ValueChangedEventArgs e)
    {
        var semiOffset = (int)Math.Round(e.NewValue);
        _driver.SelectOsc2SemiOffset(semiOffset);
        Osc2SemiValueLabel.Text = $"{semiOffset:+#;-#;0} st";
        StatusLabel.Text = $"OSC 2 semi: {semiOffset:+#;-#;0}";
    }

    private void OnOsc2WaveformChanged(object? sender, EventArgs e)
    {
        if (Osc2WaveformPicker.SelectedItem is OscillatorWaveform waveform)
        {
            _driver.SelectOsc2Waveform(waveform);
            StatusLabel.Text = $"OSC 2 wave: {waveform}";
        }
    }

    private void OnOsc2BitReduxChanged(object? sender, EventArgs e)
    {
        if (Osc2BitReduxPicker.SelectedItem is int bitRedux)
        {
            _driver.SelectOsc2BitRedux(bitRedux);
            StatusLabel.Text = $"OSC 2 BitRedux: {(bitRedux == 0 ? "Off" : bitRedux.ToString())}";
        }
    }

    private void OnOsc2KeytrackChanged(object? sender, ValueChangedEventArgs e)
    {
        var keytrackPercent = (int)Math.Round(e.NewValue);
        _driver.SelectOsc2KeytrackPercent(keytrackPercent);
        Osc2KeytrackValueLabel.Text = $"{keytrackPercent}%";
        StatusLabel.Text = $"OSC 2 keytrack: {keytrackPercent}%";
    }

    private void OnOsc2CentsChanged(object? sender, ValueChangedEventArgs e)
    {
        var centsOffset = (int)Math.Round(e.NewValue);
        _driver.SelectOsc2CentsOffset(centsOffset);
        Osc2CentsValueLabel.Text = $"{centsOffset:+#;-#;0} ct";
        StatusLabel.Text = $"OSC 2 cents: {centsOffset:+#;-#;0}";
    }

    private void OnOsc3SemiChanged(object? sender, ValueChangedEventArgs e)
    {
        var semiOffset = (int)Math.Round(e.NewValue);
        _driver.SelectOsc3SemiOffset(semiOffset);
        Osc3SemiValueLabel.Text = $"{semiOffset:+#;-#;0} st";
        StatusLabel.Text = $"OSC 3 semi: {semiOffset:+#;-#;0}";
    }

    private void OnOsc3WaveformChanged(object? sender, EventArgs e)
    {
        if (Osc3WaveformPicker.SelectedItem is OscillatorWaveform waveform)
        {
            _driver.SelectOsc3Waveform(waveform);
            StatusLabel.Text = $"OSC 3 wave: {waveform}";
        }
    }

    private void OnOsc3BitReduxChanged(object? sender, EventArgs e)
    {
        if (Osc3BitReduxPicker.SelectedItem is int bitRedux)
        {
            _driver.SelectOsc3BitRedux(bitRedux);
            StatusLabel.Text = $"OSC 3 BitRedux: {(bitRedux == 0 ? "Off" : bitRedux.ToString())}";
        }
    }

    private void OnOsc3KeytrackChanged(object? sender, ValueChangedEventArgs e)
    {
        var keytrackPercent = (int)Math.Round(e.NewValue);
        _driver.SelectOsc3KeytrackPercent(keytrackPercent);
        Osc3KeytrackValueLabel.Text = $"{keytrackPercent}%";
        StatusLabel.Text = $"OSC 3 keytrack: {keytrackPercent}%";
    }

    private void OnOsc3CentsChanged(object? sender, ValueChangedEventArgs e)
    {
        var centsOffset = (int)Math.Round(e.NewValue);
        _driver.SelectOsc3CentsOffset(centsOffset);
        Osc3CentsValueLabel.Text = $"{centsOffset:+#;-#;0} ct";
        StatusLabel.Text = $"OSC 3 cents: {centsOffset:+#;-#;0}";
    }

    private void OnOsc1VolumeChanged(object? sender, ValueChangedEventArgs e)
    {
        var volume = (float)Math.Round(e.NewValue, 1);
        _driver.SelectOsc1Volume(volume);
        Osc1VolumeLabel.Text = volume.ToString("0.0");
        StatusLabel.Text = $"OSC 1 volume: {volume:0.0}";
    }

    private void OnOsc2VolumeChanged(object? sender, ValueChangedEventArgs e)
    {
        var volume = (float)Math.Round(e.NewValue, 1);
        _driver.SelectOsc2Volume(volume);
        Osc2VolumeLabel.Text = volume.ToString("0.0");
        StatusLabel.Text = $"OSC 2 volume: {volume:0.0}";
    }

    private void OnOsc3VolumeChanged(object? sender, ValueChangedEventArgs e)
    {
        var volume = (float)Math.Round(e.NewValue, 1);
        _driver.SelectOsc3Volume(volume);
        Osc3VolumeLabel.Text = volume.ToString("0.0");
        StatusLabel.Text = $"OSC 3 volume: {volume:0.0}";
    }

    private void OnBitReduxChanged(object? sender, EventArgs e)
    {
        if (BitReduxPicker.SelectedItem is int bitRedux)
        {
            _driver.SelectBitRedux(bitRedux);
            StatusLabel.Text = $"Oscillator BitRedux: {(bitRedux == 0 ? "Off" : bitRedux.ToString())}";
        }
    }

    private void OnKeytrackChanged(object? sender, ValueChangedEventArgs e)
    {
        var keytrackPercent = (int)Math.Round(e.NewValue);
        _driver.SelectKeytrackPercent(keytrackPercent);
        KeytrackValueLabel.Text = $"{keytrackPercent}%";
        StatusLabel.Text = $"Oscillator keytrack: {keytrackPercent}%";
    }

    private void OnOscillatorLoggingToggled(object? sender, ToggledEventArgs e)
    {
        _driver.SelectOscillatorLogging(e.Value);
        StatusLabel.Text = $"Oscillator logging: {(e.Value ? "On" : "Off")}";
    }

    private void OnFilterTypeChanged(object? sender, EventArgs e)
    {
        if (FilterTypePicker.SelectedItem is FilterType filterType)
        {
            _driver.SelectFilterType(filterType);
            StatusLabel.Text = $"Filter type: {FormatFilterType(filterType)}";
            UpdateFormantVisibility();
        }
    }

    private void OnFilterCutoffChanged(object? sender, ValueChangedEventArgs e)
    {
        var cutoff = (float)Math.Round(e.NewValue, 1);
        _driver.SelectFilterCutoff(cutoff);
        FilterCutoffValueLabel.Text = cutoff.ToString("0.0");
        StatusLabel.Text = $"Filter cutoff: {cutoff:0.0}";
    }

    private void OnFilterResonanceChanged(object? sender, ValueChangedEventArgs e)
    {
        var resonance = (float)Math.Round(e.NewValue, 1);
        _driver.SelectFilterResonance(resonance);
        FilterResonanceValueLabel.Text = resonance.ToString("0.0");
        StatusLabel.Text = $"Filter resonance: {resonance:0.0}";
    }

    private void OnFilterKeytrackChanged(object? sender, ValueChangedEventArgs e)
    {
        var keytrackPercent = (int)Math.Round(e.NewValue);
        _driver.SelectFilterKeytrackPercent(keytrackPercent);
        FilterKeytrackValueLabel.Text = $"{keytrackPercent:+#;-#;0}%";
        StatusLabel.Text = $"Filter keytrack: {keytrackPercent:+#;-#;0}%";
    }

    private void OnFilterDriveChanged(object? sender, ValueChangedEventArgs e)
    {
        var drive = (float)Math.Round(e.NewValue, 1);
        _driver.SelectFilterDrive(drive);
        FilterDriveValueLabel.Text = drive.ToString("0.0");
        StatusLabel.Text = $"Filter drive: {drive:0.0}";
    }

    private void OnFilterDriveRouteChanged(object? sender, EventArgs e)
    {
        if (FilterDriveRoutePicker.SelectedItem is FilterDriveRoute driveRoute)
        {
            _driver.SelectFilterDriveRoute(driveRoute);
            StatusLabel.Text = $"Filter drive route: {driveRoute}";
        }
    }

    private void OnAudioOutputChanged(object? sender, EventArgs e)
    {
        if (AudioOutputPicker.SelectedItem is AudioDriverDeviceOption option)
        {
            _driver.SelectAudioOutputDevice(option.Id);
            StatusLabel.Text = $"Audio output: {option.Name}";
        }
    }

    private void OnSampleRateChanged(object? sender, EventArgs e)
    {
        if (SampleRatePicker.SelectedItem is int sampleRate)
        {
            _driver.SelectSampleRate(sampleRate);
            StatusLabel.Text = $"Sample rate: {sampleRate} Hz";
        }
    }

    private void OnBufferSizeChanged(object? sender, EventArgs e)
    {
        if (BufferSizePicker.SelectedItem is int bufferSize)
        {
            _driver.SelectBufferSize(bufferSize);
            StatusLabel.Text = $"Buffer size: {bufferSize}";
        }
    }

    private async void OnTestMidiClicked(object? sender, EventArgs e)
    {
        var result = await Task.Run(_driver.RunMidiTest);
        StatusLabel.Text = result;
        NoteLabel.Text = $"Test MIDI on channel {_driver.SelectedMidiChannel}";
    }

    private async void OnTestAudioClicked(object? sender, EventArgs e)
    {
        var result = await Task.Run(_driver.RunAudioTest);
        StatusLabel.Text = result;
        NoteLabel.Text = "Audio test tone played";
    }

    private void LoadSelections()
    {
        MidiInputPicker.ItemsSource = _driver.MidiInputDevices;
        AudioOutputPicker.ItemsSource = _driver.AudioOutputDevices;
        NotePlaybackModePicker.ItemsSource = _driver.NotePlaybackModes;
        WaveformPicker.ItemsSource = _driver.Waveforms;
        Osc2WaveformPicker.ItemsSource = _driver.Waveforms;
        Osc3WaveformPicker.ItemsSource = _driver.Waveforms;
        FilterTypePicker.ItemsSource = _driver.FilterTypes;
        FilterDriveRoutePicker.ItemsSource = _driver.FilterDriveRoutes;
        MidiChannelPicker.ItemsSource = _driver.MidiChannels;
        SampleRatePicker.ItemsSource = _driver.SampleRates;
        BufferSizePicker.ItemsSource = _driver.BufferSizes;
        BitReduxPicker.ItemsSource = _driver.BitReduxLevels;
        Osc2BitReduxPicker.ItemsSource = _driver.BitReduxLevels;
        Osc3BitReduxPicker.ItemsSource = _driver.BitReduxLevels;

        MidiInputPicker.SelectedItem = _driver.MidiInputDevices.FirstOrDefault(device => device.Id == _driver.SelectedMidiInputDeviceId);
        AudioOutputPicker.SelectedItem = _driver.AudioOutputDevices.FirstOrDefault(device => device.Id == _driver.SelectedAudioOutputDeviceId);
        NotePlaybackModePicker.SelectedItem = _driver.SelectedNotePlaybackMode;
        WaveformPicker.SelectedItem = _driver.SelectedWaveform;
        Osc2WaveformPicker.SelectedItem = _driver.SelectedOsc2Waveform;
        Osc3WaveformPicker.SelectedItem = _driver.SelectedOsc3Waveform;
        FilterTypePicker.SelectedItem = _driver.SelectedFilterType;
        FilterDriveRoutePicker.SelectedItem = _driver.SelectedFilterDriveRoute;
        MidiChannelPicker.SelectedItem = _driver.SelectedMidiChannel;
        SampleRatePicker.SelectedItem = _driver.SelectedSampleRate;
        BufferSizePicker.SelectedItem = _driver.SelectedBufferSize;
        BitReduxPicker.SelectedItem = _driver.SelectedBitRedux;
        Osc2BitReduxPicker.SelectedItem = _driver.SelectedOsc2BitRedux;
        Osc3BitReduxPicker.SelectedItem = _driver.SelectedOsc3BitRedux;
        VolumeSlider.Value = _driver.Volume;
        SemiStepper.Value = _driver.SelectedSemiOffset;
        CentsStepper.Value = _driver.SelectedCentsOffset;
        Osc2SemiStepper.Value = _driver.SelectedOsc2SemiOffset;
        Osc2CentsStepper.Value = _driver.SelectedOsc2CentsOffset;
        Osc3SemiStepper.Value = _driver.SelectedOsc3SemiOffset;
        Osc3CentsStepper.Value = _driver.SelectedOsc3CentsOffset;
        KeytrackSlider.Value = _driver.SelectedKeytrackPercent;
        Osc2KeytrackSlider.Value = _driver.SelectedOsc2KeytrackPercent;
        Osc3KeytrackSlider.Value = _driver.SelectedOsc3KeytrackPercent;
        Osc1VolumeSlider.Value = _driver.SelectedOsc1Volume;
        Osc2VolumeSlider.Value = _driver.SelectedOsc2Volume;
        Osc3VolumeSlider.Value = _driver.SelectedOsc3Volume;
        FilterCutoffSlider.Value = _driver.SelectedFilterCutoff;
        FilterResonanceSlider.Value = _driver.SelectedFilterResonance;
        FilterKeytrackSlider.Value = _driver.SelectedFilterKeytrackPercent;
        FilterDriveSlider.Value = _driver.SelectedFilterDrive;
        FormantVowOrderStepper.Value = _driver.SelectedFormantVowOrder;
        FormantControlSlider.Value = _driver.SelectedFormantControl;
        OscillatorLoggingSwitch.IsToggled = _driver.IsOscillatorLoggingEnabled;
        SemiValueLabel.Text = $"{_driver.SelectedSemiOffset:+#;-#;0} st";
        CentsValueLabel.Text = $"{_driver.SelectedCentsOffset:+#;-#;0} ct";
        Osc2SemiValueLabel.Text = $"{_driver.SelectedOsc2SemiOffset:+#;-#;0} st";
        Osc2CentsValueLabel.Text = $"{_driver.SelectedOsc2CentsOffset:+#;-#;0} ct";
        Osc3SemiValueLabel.Text = $"{_driver.SelectedOsc3SemiOffset:+#;-#;0} st";
        Osc3CentsValueLabel.Text = $"{_driver.SelectedOsc3CentsOffset:+#;-#;0} ct";
        KeytrackValueLabel.Text = $"{_driver.SelectedKeytrackPercent}%";
        Osc2KeytrackValueLabel.Text = $"{_driver.SelectedOsc2KeytrackPercent}%";
        Osc3KeytrackValueLabel.Text = $"{_driver.SelectedOsc3KeytrackPercent}%";
        Osc1VolumeLabel.Text = _driver.SelectedOsc1Volume.ToString("0.0");
        Osc2VolumeLabel.Text = _driver.SelectedOsc2Volume.ToString("0.0");
        Osc3VolumeLabel.Text = _driver.SelectedOsc3Volume.ToString("0.0");
        FilterCutoffValueLabel.Text = _driver.SelectedFilterCutoff.ToString("0.0");
        FilterResonanceValueLabel.Text = _driver.SelectedFilterResonance.ToString("0.0");
        FilterKeytrackValueLabel.Text = $"{_driver.SelectedFilterKeytrackPercent:+#;-#;0}%";
        FilterDriveValueLabel.Text = _driver.SelectedFilterDrive.ToString("0.0");
        FormantVowOrderValueLabel.Text = _driver.SelectedFormantVowOrder.ToString("0");
        FormantControlValueLabel.Text = _driver.SelectedFormantControl.ToString("0.0");
        UpdateFormantVisibility();
    }

    private void RefreshSelectedModule()
    {
        KeyboardSettingsPanel.IsVisible = _selectedModule == SynthModule.Keyboard;
        VoiceSettingsPanel.IsVisible = _selectedModule == SynthModule.Voice;
        Osc1SettingsPanel.IsVisible = _selectedModule == SynthModule.Osc1;
        Osc2SettingsPanel.IsVisible = _selectedModule == SynthModule.Osc2;
        Osc3SettingsPanel.IsVisible = _selectedModule == SynthModule.Osc3;
        MixerSettingsPanel.IsVisible = _selectedModule == SynthModule.Mixer;
        FilterSettingsPanel.IsVisible = _selectedModule == SynthModule.Filter;
        AmplifierSettingsPanel.IsVisible = _selectedModule == SynthModule.Amplifier;
        OutputSettingsPanel.IsVisible = _selectedModule == SynthModule.Output;

        ApplyModuleStyle(KeyboardModuleBorder, _selectedModule == SynthModule.Keyboard);
        ApplyModuleStyle(VoiceModuleBorder, _selectedModule == SynthModule.Voice);
        ApplyModuleStyle(Osc1ModuleBorder, _selectedModule == SynthModule.Osc1);
        ApplyModuleStyle(Osc2ModuleBorder, _selectedModule == SynthModule.Osc2);
        ApplyModuleStyle(Osc3ModuleBorder, _selectedModule == SynthModule.Osc3);
        ApplyModuleStyle(MixerModuleBorder, _selectedModule == SynthModule.Mixer);
        ApplyModuleStyle(FilterModuleBorder, _selectedModule == SynthModule.Filter);
        ApplyModuleStyle(AmplifierModuleBorder, _selectedModule == SynthModule.Amplifier);
        ApplyModuleStyle(OutputModuleBorder, _selectedModule == SynthModule.Output);

        var moduleDescription = GetSelectedModuleDescription();
        SelectedModuleTitleLabel.Text = moduleDescription.Title;
        SelectedModuleDescriptionLabel.Text = moduleDescription.Description;
    }

    private static void ApplyModuleStyle(Border moduleBorder, bool isSelected)
    {
        moduleBorder.Background = new SolidColorBrush(Color.FromArgb(isSelected ? "#512BD4" : "#F3F3F3"));
        moduleBorder.Stroke = new SolidColorBrush(Color.FromArgb(isSelected ? "#2B0B98" : "#ACACAC"));

        if (moduleBorder.Content is Layout layout)
        {
            foreach (var child in layout.Children.OfType<Label>())
            {
                child.TextColor = Color.FromArgb(isSelected ? "#FFFFFF" : "#1F1F1F");
            }
        }
    }

    private (string Title, string Description) GetSelectedModuleDescription()
    {
        return _selectedModule switch
        {
            SynthModule.Keyboard => ("Keyboard / MIDI", "Choose the MIDI source, channel, and trigger a MIDI diagnostics test."),
            SynthModule.Voice => ("Voice / Control", "Manage polyphony and voice modes."),
            SynthModule.Osc1 => ("Oscillator 1", "Shape oscillator 1 with tuning, waveform, bit reduction, and keyboard pitch tracking."),
            SynthModule.Osc2 => ("Oscillator 2", "Shape oscillator 2 with tuning, waveform, bit reduction, and keytracking."),
            SynthModule.Osc3 => ("Oscillator 3", "Shape oscillator 3 with tuning, waveform, bit reduction, and keytracking."),
            SynthModule.Mixer => ("Mixer", "Mix the volume of the 3 oscillators before they enter the filter."),
            SynthModule.Filter => ("Filter", "Insert a ladder low-pass filter between oscillator and amplifier with cutoff, resonance, keytracking, and drive routing."),
            SynthModule.Amplifier => ("Amplifier", "Adjust the master output level before the signal reaches the output stage."),
            SynthModule.Output => ("Output", "Configure the audio device, sample rate, buffer size, and run an audio diagnostics test."),
            _ => ("Keyboard / MIDI", "Choose the MIDI source, channel, and trigger a MIDI diagnostics test.")
        };
    }

    private static string FormatFilterType(FilterType filterType)
    {
        return filterType switch
        {
            FilterType.LpLdr12 => "LP Ldr12",
            FilterType.LpLdr14 => "LP Ldr14",
            FilterType.LpFat12 => "LP Fat12",
            FilterType.LpFat14 => "LP Fat14",
            FilterType.HpSqu24 => "TB Squ24",
            FilterType.Formant => "Vow Formant",
            _ => filterType.ToString()
        };
    }

    private void UpdateFormantVisibility()
    {
        var isFormant = _driver.SelectedFilterType == FilterType.Formant;
        FilterCutoffLabel.Text = isFormant ? "Vowel" : "Cutoff";

        FormantVowOrderLabel.IsVisible = isFormant;
        FormantVowOrderStepper.IsVisible = isFormant;
        FormantVowOrderValueLabel.IsVisible = isFormant;
        FormantControlLabel.IsVisible = isFormant;
        FormantControlSlider.IsVisible = isFormant;
        FormantControlValueLabel.IsVisible = isFormant;
    }

    private void OnFormantVowOrderChanged(object? sender, ValueChangedEventArgs e)
    {
        _driver.SelectFormantVowOrder((int)e.NewValue);
        FormantVowOrderValueLabel.Text = e.NewValue.ToString("0");
    }

    private void OnFormantControlChanged(object? sender, ValueChangedEventArgs e)
    {
        _driver.SelectFormantControl((float)e.NewValue);
        FormantControlValueLabel.Text = e.NewValue.ToString("0.0");
    }
}

