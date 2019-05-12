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
      if (!_entries.Contains(change))
      {
        _entries.Add(change);
      }
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