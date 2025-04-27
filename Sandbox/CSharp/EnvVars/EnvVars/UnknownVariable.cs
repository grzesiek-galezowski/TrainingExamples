using System;

namespace EnvVars;

public class UnknownVariable<T>
{
  public string Name => _container.Name;
  private readonly VariableContainer<T> _container;

  public static UnknownVariable<T> ProcessWide(string name, Func<string, T> parser, Func<T, string> deparser)
  {
    return new UnknownVariable<T>(VariableContainer<T>.ProcessWide(name, parser, deparser));
  }

  public static UnknownVariable<T> MachineWide(string name, Func<string, T> parser, Func<T, string> deparser)
  {
    return new UnknownVariable<T>(VariableContainer<T>.MachineWide(name, parser, deparser));
  }

  private UnknownVariable(VariableContainer<T> container)
  {
    _container = container;
  }

  public DefinedVariable<T> Read()
  {
    _container.Read();
    return new DefinedVariable<T>(_container);
  }
}

//bug add dynamic state checking as well