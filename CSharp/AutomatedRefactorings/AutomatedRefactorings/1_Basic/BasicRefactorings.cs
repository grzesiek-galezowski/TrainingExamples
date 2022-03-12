using System;
using System.Runtime.InteropServices.ComTypes;

namespace AutomatedRefactorings._1_Basic
{
  //TODO 1. rename
  //TODO 2. move
  //TODO 3. copy type
  //TODO 4. safe delete
  public class BasicRefactorings
  {
    public void Start()
    {
      Begin(1);
      Middle(2, 3);
      End(3);
    }

    private void Begin(int i)
    {
      Console.WriteLine(i);
    }
    
    private void Middle(int i, int notNeeded)
    {
      Console.WriteLine(i);
    }
    
    private void End(int i)
    {
      Console.WriteLine(i);
    }
  }

  //TODO show differences between move to separate file quick fix for VS vs R#
  //TODO refactor: move to namespace, to folder and another file
  public class DontWantToBeHere
  {
    public void Lol()
    {
      FitsSomewhereElse();
    }

    private static void FitsSomewhereElse() //TODO move to another type and make instance method
    {
      
    }
  }
}

//TODO add some real code for refactoring
