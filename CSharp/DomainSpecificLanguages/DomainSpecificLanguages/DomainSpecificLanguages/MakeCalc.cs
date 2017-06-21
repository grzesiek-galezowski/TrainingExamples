#region File Header & Copyright Notice
//Copyright 2015 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System;
using System.Threading.Tasks;

namespace DomainSpecificLanguages
{

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
  
  public class MakeCalc : TaskScript
  {
    static Task clean = Task.Simple(() => 
      Console.WriteLine("Cleaned up"));

    static Task all = Task.DependentOn(Sequence(clean, clean, clean), () => 
      Console.WriteLine("Build finished"));

    static Task allOut3 = Task.DependentOn(Parallel(clean, Sequence(clean, clean)), () => 
      Console.WriteLine("Build finished"));
  }

// ReSharper restore FieldCanBeMadeReadOnly.Local
// ReSharper restore UnusedMember.Local
// ReSharper restore InconsistentNaming

  internal class ParallelTask : ITask
  {
    private readonly ITask[] _tasks;

    public ParallelTask(ITask[] tasks)
    {
      _tasks = tasks;
    }

    public void Execute()
    {
      Parallel.ForEach(_tasks, t => t.Execute());
    }
  }

  internal class CompoundTask : ITask
  {
    private readonly Task[] _tasks;

    public CompoundTask(Task[] tasks)
    {
      _tasks = tasks;
    }

    public void Execute()
    {
      foreach (var task in _tasks)
      {
        task.Execute();
      }
    }
  }


  public class TaskScript
  {
    protected static ITask Sequence(params Task[] tasks)
    {
      return new CompoundTask(tasks);
    }

    protected static ITask Parallel(params ITask[] tasks)
    {
      return new ParallelTask(tasks);
    }
  }

  public class Task : ITask
  {
    private readonly Action _action;
    private readonly ITask _dependent;

    private Task(Action action)
    {
      _action = action;
    }

    private Task(ITask dependent, Action action1)
    {
      _dependent = dependent;
      _action = action1;
    }

    public static Task DependentOn(ITask dependencies, Action action)
    {
      return new Task(dependencies, action);
    }

    public static Task Simple(Action action)
    {
      return new Task(action);
    }

    public void Execute()
    {
      _dependent.Execute();
      _action();
    }
  }

  public interface ITask
  {
    void Execute();
  }
}
