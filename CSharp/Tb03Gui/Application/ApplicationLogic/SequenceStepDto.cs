namespace Application.ApplicationLogic;

public record SequenceStepDto
{
  public required int StepNumber { get; set; }
  public required int State { get; set; }
  public required int Note { get; set; }
  public required int Accent { get; set; }
  public required int Slide { get; set; }
}