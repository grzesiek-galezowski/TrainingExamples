using System.Collections.Immutable;

namespace Application.Ports;

public record SequenceDto(ImmutableArray<SequenceStepDto> Steps);