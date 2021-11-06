using System;

namespace EnvVars;

public class UndefinedVariableException : Exception
{
  public UndefinedVariableException(string name)
  : base($"No environment variable called {name} defined")
  {
    
  }
}

//bug introduce event log and state container so that all copies of variable representing invalid
//bug state are invalidated and show stack trace of when the state was last changed