namespace EnvVars;

public class StoredVariable<T>
{
  private readonly VariableContainer<T> _variableContainer;

  public StoredVariable(VariableContainer<T> variableContainer)
  {
    _variableContainer = variableContainer;
  }

  public string Name => _variableContainer.Name;

  public PotentialVariable<T> Delete()
  {
    _variableContainer.Delete();
    return new PotentialVariable<T>(_variableContainer);
  }

  public StoredVariable<T> ChangeValue(T value)
  {
    _variableContainer.ChangeValue(value);
    return new StoredVariable<T>(_variableContainer);
  }

  public T Value() => _variableContainer.Value.Value;
}