using System.Linq;

namespace ChangingBehaviorThroughComposition
{
  public class CompoundSwitchCriteria : SwitchCriteria
  {
    private readonly SwitchCriteria[] _switchCriterias;

    public CompoundSwitchCriteria(params SwitchCriteria[] switchCriterias)
    {
      _switchCriterias = switchCriterias;
    }

    public bool IsNotMet()
    {
      return _switchCriterias.All(criteria => criteria.IsNotMet());
    }
  }
}