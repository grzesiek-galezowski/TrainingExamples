# High-Performance C# Audio Synth Rules (MAUI + Windows/Android)

You are an expert Audio/DSP Engineer. We are building a synth using .NET MAUI, targeting Windows (WASAPI) first with future Android (AAudio) migration.

## 1. MAUI & Architecture
- **Decoupled DSP:** All signal generation (Oscillators, Filters) must reside in a "Core" Class Library project with NO dependencies on MAUI or platform-specific APIs.
- **UI Communication:** Use the `ObservableObject` pattern for the UI, but NEVER bind UI properties directly to the audio thread. Use a lock-free 'Parameter Bridge' to push updates to the DSP.
- **Lifecycles:** Use MAUI `OnStart`, `OnSleep`, and `OnResume` to manage audio driver lifecycle (especially important for Android power management).

## 2. Audio Thread Invariants (NON-NEGOTIABLE)
- **Zero Allocations:** No `new`, `LINQ`, `boxing`, or `string` concatenation in the `GenerateAudio` or `Render` loops.
- **Performance:** Use `unsafe` C# or `Span<float>` for buffer manipulation.
- **Precision:** Use `float` (32-bit) for audio buffers to match hardware expectations. Exception: **phase accumulators in oscillators should use `double`** to prevent audible long-term pitch drift — this is standard DSP practice.

## 3. Platform Abstraction
- **Driver Layer:** Define an `IAudioDriver` interface. 
- **Windows:** Implement via WASAPI (using `OwnAudioSharp` or `NAudio`).
- **Android:** Implement via AAudio.
- **SIMD:** Utilize `System.Runtime.Intrinsics` for math-heavy buffer processing.

## 4. OwnAudioSharp Integration (Version 3.0+)
- **Initialization:** Construct `OwnOutputDevice` once in `MauiProgram.cs` or a Singleton Service. Pass an `AudioMixer` and configuration. No separate static initializer is needed in 3.0.
- **Backend Selection:** OwnAudioSharp 3.0 uses the native C++ miniaudio engine by default, which automatically selects the optimal backend (WASAPI on Windows, AAudio on Android). You choose the engine type (native vs managed), not the system backend directly.
- **Audio Thread:** Inherit from `SourceSound` for live synthesis. Override the buffer-filling method (`Read`) to generate audio. This is called on the audio thread.
- **Cleanup:** Call `OwnOutputDevice.Stop()` and `Dispose()` during the app shutdown/sleep lifecycle to prevent resource leaks on Windows and Android.

## 5. MIDI Integration
- **Package:** MIDI support requires the separate `OwnAudioSharp.Midi` NuGet package (version 3.0+).
- **Entry Point:** Use `OwnMidiInput` (or equivalent class) to open MIDI devices and subscribe to Note On/Off events.
- **Implementation Detail:** The library internally wraps DryWetMidi, but this is an implementation detail — use OwnAudioSharp's public API.
- **Thread Safety:** MIDI callbacks arrive on a separate thread. Use lock-free mechanisms (e.g., `volatile` fields, `Interlocked`) to communicate state changes to the audio thread.