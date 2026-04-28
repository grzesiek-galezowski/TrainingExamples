using MutaSynthEngine;

namespace MutaSynth;

public partial class MainPage : ContentPage
{
    private enum SynthModule
    {
        Keyboard,
        Oscillator,
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

    private void OnOscillatorModuleTapped(object? sender, TappedEventArgs e)
    {
        _selectedModule = SynthModule.Oscillator;
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
        FilterTypePicker.ItemsSource = _driver.FilterTypes;
        FilterDriveRoutePicker.ItemsSource = _driver.FilterDriveRoutes;
        MidiChannelPicker.ItemsSource = _driver.MidiChannels;
        SampleRatePicker.ItemsSource = _driver.SampleRates;
        BufferSizePicker.ItemsSource = _driver.BufferSizes;
        BitReduxPicker.ItemsSource = _driver.BitReduxLevels;

        MidiInputPicker.SelectedItem = _driver.MidiInputDevices.FirstOrDefault(device => device.Id == _driver.SelectedMidiInputDeviceId);
        AudioOutputPicker.SelectedItem = _driver.AudioOutputDevices.FirstOrDefault(device => device.Id == _driver.SelectedAudioOutputDeviceId);
        NotePlaybackModePicker.SelectedItem = _driver.SelectedNotePlaybackMode;
        WaveformPicker.SelectedItem = _driver.SelectedWaveform;
        FilterTypePicker.SelectedItem = _driver.SelectedFilterType;
        FilterDriveRoutePicker.SelectedItem = _driver.SelectedFilterDriveRoute;
        MidiChannelPicker.SelectedItem = _driver.SelectedMidiChannel;
        SampleRatePicker.SelectedItem = _driver.SelectedSampleRate;
        BufferSizePicker.SelectedItem = _driver.SelectedBufferSize;
        BitReduxPicker.SelectedItem = _driver.SelectedBitRedux;
        VolumeSlider.Value = _driver.Volume;
        SemiStepper.Value = _driver.SelectedSemiOffset;
        CentsStepper.Value = _driver.SelectedCentsOffset;
        KeytrackSlider.Value = _driver.SelectedKeytrackPercent;
        FilterCutoffSlider.Value = _driver.SelectedFilterCutoff;
        FilterResonanceSlider.Value = _driver.SelectedFilterResonance;
        FilterKeytrackSlider.Value = _driver.SelectedFilterKeytrackPercent;
        FilterDriveSlider.Value = _driver.SelectedFilterDrive;
        OscillatorLoggingSwitch.IsToggled = _driver.IsOscillatorLoggingEnabled;
        SemiValueLabel.Text = $"{_driver.SelectedSemiOffset:+#;-#;0} semitones";
        CentsValueLabel.Text = $"{_driver.SelectedCentsOffset:+#;-#;0} cents";
        KeytrackValueLabel.Text = $"{_driver.SelectedKeytrackPercent}%";
        FilterCutoffValueLabel.Text = _driver.SelectedFilterCutoff.ToString("0.0");
        FilterResonanceValueLabel.Text = _driver.SelectedFilterResonance.ToString("0.0");
        FilterKeytrackValueLabel.Text = $"{_driver.SelectedFilterKeytrackPercent:+#;-#;0}%";
        FilterDriveValueLabel.Text = _driver.SelectedFilterDrive.ToString("0.0");
    }

    private void RefreshSelectedModule()
    {
        KeyboardSettingsPanel.IsVisible = _selectedModule == SynthModule.Keyboard;
        OscillatorSettingsPanel.IsVisible = _selectedModule == SynthModule.Oscillator;
        FilterSettingsPanel.IsVisible = _selectedModule == SynthModule.Filter;
        AmplifierSettingsPanel.IsVisible = _selectedModule == SynthModule.Amplifier;
        OutputSettingsPanel.IsVisible = _selectedModule == SynthModule.Output;

        ApplyModuleStyle(KeyboardModuleBorder, _selectedModule == SynthModule.Keyboard);
        ApplyModuleStyle(OscillatorModuleBorder, _selectedModule == SynthModule.Oscillator);
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
            SynthModule.Oscillator => ("Oscillator", "Shape the oscillator with tuning, waveform, bit reduction, and keyboard pitch tracking."),
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
            _ => filterType.ToString()
        };
    }
}

