﻿using Midi.Devices;
using Midi.Enums;

namespace MidiPlayground;

public class Synthesizer : IDisposable
{
  private readonly IOutputDevice _outputDevice;
  private TimeSpan _delay;
  private readonly Channel _currentChannel = Channel.Channel1;

  public Synthesizer(IOutputDevice outputDevice)
  {
    _outputDevice = outputDevice;
  }

  public void TurnOn()
  {
    _outputDevice.Open();
    _outputDevice.SendProgramChange(_currentChannel, Instrument.SynthBass1);
  }

  private static TimeSpan CalculateDelayForBpm(double bpm)
  {
    var tpb = 4;
    var delay = TimeSpan.FromSeconds(60d / (bpm * tpb));
    return delay;
  }

  public void SetBpm(double bpm)
  {
    _delay = CalculateDelayForBpm(bpm);
  }

  public static Synthesizer Create()
  {
    var synth = new Synthesizer(DeviceManager.OutputDevices[0]);
    synth.TurnOn();
    synth.SetBpm(120);
    return synth;
  }

  public void Play(List<Pitch> melody)
  {
    foreach (var pitch in melody)
    {
      _outputDevice.SendNoteOn(_currentChannel, pitch, 80);
      Thread.Sleep(_delay);
      _outputDevice.SendNoteOff(_currentChannel, pitch, 80);
    }
  }

  public void Dispose()
  {
    _outputDevice.Close();
  }
}