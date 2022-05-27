namespace Lib;

public static class FederalDatabase
{
  public static string FindCar(LicensePlateQueryData queryData)
  {
    return queryData switch
    {
      { State: "Alabama", Number: "ABC1234" } => "Alfa Romeo",
      { State: "Alabama", Number: "ABC1235" } => "Cadillac",
      { State: "New York", Number: "ABC12" } => "Rolls Royce",
      _ => "Unknown"
    };
  }
}