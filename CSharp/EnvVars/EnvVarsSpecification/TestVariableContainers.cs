using EnvVars;

namespace EnvVarsSpecification;

internal static class TestVariableContainers
{
  public static VariableContainer<string> VariableContainerWithValue(string name)
  {
    var container = VariableContainer<string>.ProcessWide(name, s => s, s => s);
    container.Read();
    return container;
  }
}