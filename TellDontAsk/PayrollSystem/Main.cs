
namespace PayrollSystem
{
  public class Main
  {
    public Main()
    {
      var policies = new CompanyPolicies();
      policies.ApplyYearlyIncentivePlan();
      policies.Dispose();
    }
  }
}

