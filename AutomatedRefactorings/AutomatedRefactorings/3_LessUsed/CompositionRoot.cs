using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedRefactorings._3_LessUsed
{
  class CompositionRoot
  {
    public MySystem Resolve()
    {
      return new ExampleSystem(new Object1(), new Object2(), new Object3());
    }
  }

  class ExampleSystem : MySystem
  {
    private readonly Object1 _object1;
    private readonly Object2 _object2;
    private readonly Object3 _object3;

    public ExampleSystem(Object1 object1, Object2 object2, Object3 object3)
    {
      _object1 = object1;
      _object2 = object2;
      _object3 = object3;
    }

    public void Lol()
    {
      _object1.DoSomething();
      _object2.DoSomething();
      _object3.DoSomething();
    }
  }

  interface MySystem
  {
  }
}
