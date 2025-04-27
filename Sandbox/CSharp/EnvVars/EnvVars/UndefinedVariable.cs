using System;

namespace EnvVars;

public class UndefinedVariable<T>
{
  private readonly VariableContainer<T> _container;

  internal UndefinedVariable(VariableContainer<T> variableContainer)
  {
    _container = variableContainer;
  }

  public static UndefinedVariable<T> ProcessWide(
    string name, 
    Func<string, T> parser, 
    Func<T, string> deparser)
  {
    return new UndefinedVariable<T>(
      VariableContainer<T>.ProcessWide(name, parser, deparser));
  }

  public static UndefinedVariable<T> MachineWide(
    string name, 
    Func<string, T> parser, 
    Func<T, string> deparser)
  {
    return new UndefinedVariable<T>(
      VariableContainer<T>.MachineWide(name, parser, deparser));
  }

  public string Name => _container.Name;

  public DefinedVariable<T> Define(T value)
  {
    _container.Define(value);
    return new DefinedVariable<T>(_container);
  }
}