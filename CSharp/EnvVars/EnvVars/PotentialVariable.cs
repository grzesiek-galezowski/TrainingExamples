using System;

namespace EnvVars;

public class PotentialVariable<T>
{
  private readonly VariableContainer<T> _container;

  public PotentialVariable(VariableContainer<T> variableContainer)
  {
    _container = variableContainer;
  }

  public static PotentialVariable<T> ProcessWide(
    string name, 
    Func<string, T> parser, 
    Func<T, string> deparser)
  {
    return new PotentialVariable<T>(
      VariableContainer<T>.ProcessWide(name, parser, deparser));
  }

  public static PotentialVariable<T> MachineWide(
    string name, 
    Func<string, T> parser, 
    Func<T, string> deparser)
  {
    return new PotentialVariable<T>(
      VariableContainer<T>.MachineWide(name, parser, deparser));
  }

  public string Name => _container.Name;

  public StoredVariable<T> Define(T value)
  {
    _container.Define(value);
    return new StoredVariable<T>(_container);
  }
}