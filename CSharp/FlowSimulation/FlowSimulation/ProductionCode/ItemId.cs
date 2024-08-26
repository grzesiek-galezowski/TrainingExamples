namespace FlowSimulation.ProductionCode;

public record ItemId(string Text)
{
  public override string ToString() => Text;

  public static implicit operator ItemId(string text) => new(text); //bug need this?
}