/**
 * Created by astral on 24.03.15.
 */
public class Employee
{
  public void Save()
  {
    System.out.println("Saving");
  }

  public void SetBonusForYear(int i, int oneTimeBonus)
  {
    System.out.printf("Setting for %d to %d \n", i, oneTimeBonus);
  }

  public int GetYearsOfService()
  {
    return 123;
  }

  public void SetSalary(int newSalary)
  {
    System.out.println("New Salary is: " + newSalary);
  }

  public PayGrade GetPayGrade()
  {
    return new PayGrade();
  }

  public int GetSalary()
  {
    return 44;
  }
}