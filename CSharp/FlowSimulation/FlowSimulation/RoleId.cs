namespace FlowSimulation;

public record RoleId(string Text)
{
  public override string ToString() => Text;

  public static implicit operator RoleId(string text) => new(text);
}