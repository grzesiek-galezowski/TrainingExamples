using NSubstitute;

namespace IoCContainerPros
{
  public class LogicRoot
  {
    private readonly SomeLogic _someLogic;

    public LogicRoot()
    {
      _someLogic = new SomeLogic(CreateTroublesomeDependency());
    }

    protected virtual ITroublesomeDependency CreateTroublesomeDependency()
    {
      return new TroublesomeDependency();
    }

    public SomeLogic GetSomeLogic()
    {
      return _someLogic;
    }
  }

  public class LogicRootForTests : LogicRoot
  {
    protected override ITroublesomeDependency CreateTroublesomeDependency()
    {
      return TroublesomeDependency;
    }

    public ITroublesomeDependency TroublesomeDependency { get; } 
      = Substitute.For<ITroublesomeDependency>();
  }
}