using MockNoMock;

namespace MockNoMockSpecification._01_ChangeDetectors;

public class WorkProcessing
{
  private readonly IFirstPart _firstPart;
  private readonly ISecondPart _secondPart;

  public WorkProcessing(IFirstPart firstPart, ISecondPart secondPart)
  {
    _firstPart = firstPart;
    _secondPart = secondPart;
  }

  public void Process(Work work)
  {
    _firstPart.Process(work);
    _secondPart.Process(work);
  }
}