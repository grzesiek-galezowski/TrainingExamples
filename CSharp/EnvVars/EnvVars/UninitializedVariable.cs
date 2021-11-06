using System;

namespace EnvVars;

public class UninitializedVariable<T>
{
  public string Name => _container.Name;
  private readonly VariableContainer<T> _container;

  public UninitializedVariable(string name, Func<string, T> parser, Func<T, string> deparser)
  {
    _container = VariableContainer<T>.ProcessWide(name, parser, deparser);
  }

  public StoredVariable<T> Read()
  {
    _container.Read();
    return new StoredVariable<T>(_container);
  }
}

//UninitializedVariable<T> => read => StoredVariable<T>
//PotentialVariable<T> => define => StoredVariable<T>
//StoredVariable<T> => delete => PotentialVariable<T>
//StoredVariable<T> => overwrite => StoredVariable<T>
//bug add dynamic state checking as well