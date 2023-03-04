namespace UnitTestRunnerPackageExercise;

public class FrenchConsoleMessages : IConsoleReportMessages
{
  public string TestPassed(FullyQualifiedTestName fullyQualifiedTestName)
  {
    return $"Réussi : {fullyQualifiedTestName}";
  }

  public string EndOfSuite(string suiteName)
  {
    return "Fin du groupe de tests " + suiteName;
  }

  public string TestFailed(FullyQualifiedTestName fullyQualifiedTestName,
    Exception failureRootCause)
  {
    return $"Échec : {fullyQualifiedTestName}{Environment.NewLine}{failureRootCause}";
  }

  public string StartingTest(FullyQualifiedTestName fullyQualifiedTestName)
  {
    return $"Démarrage du test : " + $"{fullyQualifiedTestName}";
  }

  public string StartingSuite(string suiteName)
  {
    return "Démarrage du groupe de tests " + suiteName;
  }
}