using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationLogic
{
  public class AnalysisResult
  {
    public string Path { get; }
    private readonly IEnumerable<ChangeLog> _changeLogs;

    public AnalysisResult(IEnumerable<ChangeLog> changeLogs, string path)
    {
      Path = path.Replace("\\", "/");
      _changeLogs = changeLogs;
      UpdateChangeCountRanking();
      UpdateComplexityRanking();
    }

    public IEnumerable<ChangeLog> EntriesByHotSpotRank()
    {
      return _changeLogs.OrderByDescending(h => h.HotSpotRank());
    }

    private void UpdateComplexityRanking()
    {
      EntriesByRisingComplexity()
        .Select(WithIndex())
        .ToList().ForEach(tuple => tuple.entry.AssignComplexityRank(tuple.index));
    }

    private static Func<ChangeLog, int, (ChangeLog entry, int index)> WithIndex()
    {
      return (entry, index) => (entry, index: index + 1);
    }

    public IEnumerable<ChangeLog> EntriesByRisingComplexity()
    {
      return _changeLogs.OrderBy(h => h.ComplexityOfCurrentVersion());
    }

    public IEnumerable<ChangeLog> EntriesByDiminishingComplexity()
    {
      return EntriesByRisingComplexity().Reverse();
    }

    private void UpdateChangeCountRanking()
    {
      EntriesByRisingChangesCount()
        .Select(WithIndex())
        .ToList().ForEach(tuple => tuple.entry.AssignChangeCountRank(tuple.index));
    }

    public IEnumerable<ChangeLog> EntriesByRisingChangesCount()
    {
      return _changeLogs.OrderBy(h => h.ChangesCount());
    }

    public IEnumerable<ChangeLog> EntriesByDiminishingChangesCount()
    {
      return _changeLogs.OrderByDescending(h => h.ChangesCount());
    }

    public IEnumerable<ChangeLog> EntriesByDiminishingActivityPeriod()
    {
      return _changeLogs.OrderByDescending(h => h.ActivityPeriod());
    }

    public IEnumerable<ChangeLog> EntriesFromMostRecentlyChanged()
    {
      return _changeLogs.OrderByDescending(h => h.LastChangeDate());
    }
    public IEnumerable<ChangeLog> EntriesFromMostAncientlyChanged()
    {
      return EntriesFromMostRecentlyChanged().Reverse();
    }
  }
}