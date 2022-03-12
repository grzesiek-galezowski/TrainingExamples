using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedRefactorings._3_LessUsed
{
  //TODO replace constructor with factory method, make constructor public again
  //TODO Move static factory method to MyMessageFactory (new class)
  //TODO replace static method with instance one (make non-static trick or quick fix)
  //TODO use MyMessageFactory
  //TODO inline method
  //TODO for factory - introduce field (initialized from constructor)
  //TODO for factory constructor invocation - introduce parameter
  //TODO for factory - extract interface
  //TODO for factory - use interface where possible

  public class Object1
  {
    public void DoSomething()
    {
      var message = new MyMessage(1, 2);
      message.Send();
    }
  }

  public class Object2
  {
    public void DoSomething()
    {
      var message = new MyMessage(7,8);
      message.Send();
    }
  }

  public class Object3
  {
    public void DoSomething()
    {
      var message = new MyMessage(5,6);
      message.Send();
    }
  }
}
