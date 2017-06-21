using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedRefactorings._3_LessUsed
{
  //TODO replace constructor with factory method
  //TODO copy type MyMessage with factory method / move static method
  //TODO replace static method with instance one (not automated)
  //TODO change returned type and rename to MyMessageFactory
  //TODO use MyMessageFactory
  //TODO inline method
  //TODO for factory - introduce field (initialized from constructor)
  //TODO for factory constructor invocation - introduce parameter
  //TODO for factory - extract interface
  //TODO for factory - use base type where possible

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
