using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedRefactorings._3_LessUsed
{
  public class Object1
  {
    public void DoSomething()
    {
      var message = new MyMessage(1, 2);
    }
  }

  public class Object2
  {
    public void DoSomething()
    {
      var message = new MyMessage(7,8);
    }
  }

  public class Object3
  {
    public void DoSomething()
    {
      var message = new MyMessage(5,6);
    }
  }

  public class MyMessage
  {
    private readonly int _i;
    private readonly int _i1;

    public MyMessage(int i, int i1)
    {
      _i = i;
      _i1 = i1;
    }

    public int Prop1
    {
      get { return _i; }
    }

    public int Prop2
    {
      get { return _i1; }
    }
  }
}
