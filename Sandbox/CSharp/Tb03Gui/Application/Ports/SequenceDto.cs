using System.Collections.Immutable;

namespace Application.Ports;

public record SequenceDto(
  int EndStep,
  int Triplet,
  ImmutableArray<SequenceStepDto> Steps);