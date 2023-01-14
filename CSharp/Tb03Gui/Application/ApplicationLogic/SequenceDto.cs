using System.Collections.Immutable;

namespace Application.ApplicationLogic;

public record SequenceDto(ImmutableArray<SequenceStepDto> Steps);