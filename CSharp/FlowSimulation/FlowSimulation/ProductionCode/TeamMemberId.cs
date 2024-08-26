namespace FlowSimulation.ProductionCode;

public record TeamMemberId(string Text)
{
  public override string ToString() => Text;

  public static implicit operator TeamMemberId(string text) => new(text);
}