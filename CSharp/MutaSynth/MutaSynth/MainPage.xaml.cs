using MutaSynthEngine;

namespace MutaSynth;

public partial class MainPage : ContentPage
{
    private enum SynthModule
    {
        Keyboard,
        Oscillator,
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
        MidiChannelPicker.ItemsSource = _driver.MidiChannels;
        SampleRatePicker.ItemsSource = _driver.SampleRates;
        BufferSizePicker.ItemsSource = _driver.BufferSizes;

        MidiInputPicker.SelectedItem = _driver.MidiInputDevices.FirstOrDefault(device => device.Id == _driver.SelectedMidiInputDeviceId);
        AudioOutputPicker.SelectedItem = _driver.AudioOutputDevices.FirstOrDefault(device => device.Id == _driver.SelectedAudioOutputDeviceId);
        NotePlaybackModePicker.SelectedItem = _driver.SelectedNotePlaybackMode;
        MidiChannelPicker.SelectedItem = _driver.SelectedMidiChannel;
        SampleRatePicker.SelectedItem = _driver.SelectedSampleRate;
        BufferSizePicker.SelectedItem = _driver.SelectedBufferSize;
        VolumeSlider.Value = _driver.Volume;
    }

    private void RefreshSelectedModule()
    {
        KeyboardSettingsPanel.IsVisible = _selectedModule == SynthModule.Keyboard;
        OscillatorSettingsPanel.IsVisible = _selectedModule == SynthModule.Oscillator;
        AmplifierSettingsPanel.IsVisible = _selectedModule == SynthModule.Amplifier;
        OutputSettingsPanel.IsVisible = _selectedModule == SynthModule.Output;

        ApplyModuleStyle(KeyboardModuleBorder, _selectedModule == SynthModule.Keyboard);
        ApplyModuleStyle(OscillatorModuleBorder, _selectedModule == SynthModule.Oscillator);
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
            SynthModule.Oscillator => ("Oscillator", "Set how incoming notes are handled by the synth voice engine."),
            SynthModule.Amplifier => ("Amplifier", "Adjust the master output level before the signal reaches the output stage."),
            SynthModule.Output => ("Output", "Configure the audio device, sample rate, buffer size, and run an audio diagnostics test."),
            _ => ("Keyboard / MIDI", "Choose the MIDI source, channel, and trigger a MIDI diagnostics test.")
        };
    }
}

