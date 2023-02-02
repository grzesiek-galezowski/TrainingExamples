using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Ports;
using Midi.Devices;
using Midi.Enums;

namespace Tb03Gui.Midi;

public class Synthesizer : IDisposable, ISynthesizer
{
  private IOutputDevice _outputDevice;
  private TimeSpan _delay;
  private readonly Channel _currentChannel = Channel.Channel1;

  private Synthesizer(IOutputDevice outputDevice)
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

  public async Task Play(List<int> melody, CancellationToken cancellationToken)
  {
    for (var index = 0; 
         index < melody.Count && !cancellationToken.IsCancellationRequested; 
         index++)
    {
      var pitch = melody[index];
      _outputDevice.SendNoteOn(_currentChannel, (Pitch)pitch, 80);
      await Task.Delay(_delay, CancellationToken.None);
      _outputDevice.SendNoteOff(_currentChannel, (Pitch)pitch, 80);
    }
  }

  public void ChangeOutputDevice(string deviceName)
  {
    if (deviceName != _outputDevice.Name)
    {
      //bug maybe don't change the internal state. Instead, dispose the old synth and create a new synth with the name
      //bug what about synchronization? The device can be disposed while playing.
      _outputDevice.Close();
      _outputDevice = DeviceManager.OutputDevices.Single(d => d.Name == deviceName);
      TurnOn();
    }
  }

  public void Dispose()
  {
    _outputDevice.Close();
  }

  public static ImmutableArray<string> GetMidiDevices()
  {
    return DeviceManager.OutputDevices.Select(d => d.Name).ToImmutableArray();
  }

  public string CurrentMidiDevice()
  {
    return _outputDevice.Name;
  }
}