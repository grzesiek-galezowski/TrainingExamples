import java.io.Closeable;
import java.io.IOException;

/**
 * Created by astral on 24.03.15.
 */
//TODO refactor this class to add new employee type: contractor
//|---------------------|------------------------------|---------------------------|
//| Employee Type       | Raise                        |     Bonus                 |
//|---------------------|------------------------------|---------------------------|
//| Regular Employee    | +10% of current salary       | +200% of current salary   |
//|                     | if not reached maximum       | one time after five years |
//|                     | on a given pay grade         |                           |
//|---------------------|------------------------------|---------------------------|
//| Contractor          | +5% of average salary        | +10% of current salary    |
//|                     | calculated for last 3        | when a contractor receives|
//|                     | years of service (or         | score more than 100 for   |
//|                     | all previous years of        | the previous year         |
//|                     | service if they have         |                           |
//|                     | worked for less than 3 years |                           |


public class CompanyPolicies implements Closeable {
  final SqlRepository _repository = new SqlRepository();

  public void ApplyYearlyIncentivePlan() {
    Iterable<Employee> employees = _repository.CurrentEmployees();

    for (Employee employee : employees) {
      //evaluate raise
      if (employee.GetSalary() < employee.GetPayGrade().getMaximum()) {
        int newSalary = (int) (employee.GetSalary() + (float)employee.GetSalary() * 0.1);

        employee.SetSalary(newSalary);
      }

      //evaluate one-time bonus
      if (employee.GetYearsOfService() == 5) {
        int oneTimeBonus = employee.GetSalary() * 2;
        employee.SetBonusForYear(2014, oneTimeBonus);
      }

      //store the employee
      employee.Save();
    }
  }

  @Override
  public void close() throws IOException {
    _repository.Dispose();
  }
}