using Ownaudio.Core;
using OwnaudioNET;
using OwnaudioNET.Mixing;
using OwnaudioNET.Sources;
using System;
using System.Threading;

class Program
{
  static void Main()
  {
    // Initialize engine
    OwnaudioNet.Initialize();
    var engine = OwnaudioNet.Engine!;
    var mixer = new AudioMixer(engine.UnderlyingEngine);
    mixer.Start();

    int sampleRate = engine.Config.SampleRate;
    int channels = engine.Config.Channels;
    int frames = 1024;

    // Dynamic SampleSource (buffer size = frames)
    var source = new SampleSource(frames, new AudioConfig
    {
      SampleRate = sampleRate,
      Channels = channels
    });

    mixer.AddSource(source);

    Console.WriteLine("Press SPACE to toggle sine. ESC to exit.");

    bool playing = false;
    bool exit = false;

    Thread audioThread = new Thread(() =>
    {
      double phase = 0;
      double freq = 440.0;
      double inc = 2 * Math.PI * freq / sampleRate;

      float[] buffer = new float[frames * channels];

      while (!exit)
      {
        if (playing)
        {
          for (int i = 0; i < frames; i++)
          {
            float s = (float)Math.Sin(phase);
            phase += inc;
            if (phase >= Math.PI * 2) phase -= Math.PI * 2;

            for (int ch = 0; ch < channels; ch++)
              buffer[i * channels + ch] = s;
          }

          // This is essential: continuously refill the buffer
          source.SubmitSamples(buffer);
        }

        // Prevent CPU burn
        Thread.Sleep(1);
      }
    });

    audioThread.Start();

    while (true)
    {
      var key = Console.ReadKey(true).Key;

      if (key == ConsoleKey.Spacebar)
      {
        playing = !playing;

        if (playing)
        {
          source.Play();   // MUST be playing or SampleSource returns silence
          Console.WriteLine("Sine ON");
        }
        else
        {
          source.Stop();
          Console.WriteLine("Sine OFF");
        }
      }

      if (key == ConsoleKey.Escape)
        break;
    }

    exit = true;
    audioThread.Join();

    mixer.Stop();
    OwnaudioNet.Shutdown();
  }
}
