using System.Collections.Immutable;

namespace MidiPlayground;

public record SequenceDto(ImmutableArray<SequenceStepDto> Steps);