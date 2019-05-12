using System.Collections.Generic;
using System.Linq;

namespace ApplicationLogic
{
  public class ChangeLog
  {
    private readonly List<Change> _entries = new List<Change>();

    public IReadOnlyList<Change> Entries => _entries;

    public void AddDataFrom(Change change)
    {
      if (!_entries.Any())
      {
        _entries.Add(change);
      }
      else if (!IsDuplicatedForSomeReason(change))
      {
        _entries.Add(change);
      }
    }

    private bool IsDuplicatedForSomeReason(Change change)
    {
      return _entries.Last().Text.Equals(change.Text);
    }

    public string PathOfLastVersion()
    {
      return Entries.Last().Path;
    }

    public int ChangesCount()
    {
      return Entries.Count;
    }

    public double ComplexityOfLastVersion()
    {
      return Entries.Last().Complexity;
    }
  }
}