using Midi.Devices;
using Midi.Enums;

namespace MidiPlayground;

internal class Synth : IDisposable
{
  private readonly IOutputDevice _outputDevice;
  private TimeSpan _delay;
  private readonly Channel _currentChannel = Channel.Channel1;

  public Synth(IOutputDevice outputDevice)
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

  public static Synth Create()
  {
    return new Synth(DeviceManager.OutputDevices[0]);
  }

  public async Task Play(List<Pitch> melody)
  {
    foreach (var pitch in melody)
    {
      _outputDevice.SendNoteOn(_currentChannel, pitch, 80);
      await Task.Delay(_delay);
    }
  }

  public void Dispose()
  {
    _outputDevice.Close();
  }
}