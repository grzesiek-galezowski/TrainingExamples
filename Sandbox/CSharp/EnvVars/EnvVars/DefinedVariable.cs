namespace EnvVars;

public class DefinedVariable<T>
{
  private readonly VariableContainer<T> _variableContainer;

  internal DefinedVariable(VariableContainer<T> variableContainer)
  {
    _variableContainer = variableContainer;
  }

  public string Name => _variableContainer.Name;

  public UndefinedVariable<T> Delete()
  {
    _variableContainer.Delete();
    return new UndefinedVariable<T>(_variableContainer);
  }

  public DefinedVariable<T> ChangeValue(T value)
  {
    _variableContainer.ChangeValue(value);
    return new DefinedVariable<T>(_variableContainer);
  }

  public T Value() => _variableContainer.Value.Value;
}