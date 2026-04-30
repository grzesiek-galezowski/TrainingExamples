namespace MutaSynthEngine.Platforms.Windows;

public sealed class Mixer
{
    public float Osc1Volume { get; set; } = 128.0f;
    public float Osc2Volume { get; set; } = 0.0f;
    public float Osc3Volume { get; set; } = 0.0f;

    public float Process(float osc1Sample, float osc2Sample, float osc3Sample)
    {
        var osc1Mix = Osc1Volume / 128.0f;
        var osc2Mix = Osc2Volume / 128.0f;
        var osc3Mix = Osc3Volume / 128.0f;

        return (osc1Sample * osc1Mix) + (osc2Sample * osc2Mix) + (osc3Sample * osc3Mix);
    }
}
