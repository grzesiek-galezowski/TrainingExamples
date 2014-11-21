﻿using System;

namespace AutomatedRefactorings.Basic
{
  // 1. rename
  // 2. move
  // 3. copy type
  // 4. safe delete
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

  public class DontWantToBeHere //TODO move it to another namespace and another file
  {
    
  }

  //TODO add some real code for refactoring
}
