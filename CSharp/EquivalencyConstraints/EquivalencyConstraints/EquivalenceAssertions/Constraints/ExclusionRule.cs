namespace EquivalencyConstraints.EquivalenceAssertions.Constraints;

public class ExclusionRule(string path)
{
  public string Path => path;
  public List<string> ExcludedProperties { get; } = new();
  public bool IgnoreOrder { get; set; }
}