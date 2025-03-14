namespace DependencyUpdatePriorityScanner.EquivalenceAssertions.Constraints;

public class ExclusionRule
{
    public string Path { get; }
    public List<string> ExcludedProperties { get; } = new();
    public bool IgnoreOrder { get; set; }

    public ExclusionRule(string path)
    {
        Path = path;
    }
}