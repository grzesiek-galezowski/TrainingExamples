using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationLogic;

namespace GitAttempt2
{
  public class AnalysisResult
  {
    private readonly IEnumerable<ChangeLog> _changeLogs;

    public AnalysisResult(IEnumerable<ChangeLog> changeLogs)
    {
      _changeLogs = changeLogs;
      UpdateChangeCountRanking();
      UpdateComplexityRanking();
    }

    public ChangeLog[] EntriesByHotSpotRank()
    {
      return _changeLogs.OrderByDescending(h => h.HotSpotRank()).ToArray();
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

    public ChangeLog[] EntriesByRisingComplexity()
    {
      return _changeLogs.OrderBy(h => h.ComplexityOfCurrentVersion()).ToArray();
    }

    public ChangeLog[] EntriesByDiminishingComplexity()
    {
      return EntriesByRisingComplexity().Reverse().ToArray();
    }

    private void UpdateChangeCountRanking()
    {
      EntriesByRisingChangesCount()
        .Select(WithIndex())
        .ToList().ForEach(tuple => tuple.entry.AssignChangeCountRank(tuple.index));
    }

    public ChangeLog[] EntriesByRisingChangesCount()
    {
      return _changeLogs.OrderBy(h => h.ChangesCount()).ToArray();
    }

    public ChangeLog[] EntriesByDiminishingChangesCount()
    {
      return _changeLogs.OrderBy(h => h.ChangesCount()).ToArray();
    }

    public ChangeLog[] EntriesByDiminishingActivityPeriod()
    {
      return _changeLogs.OrderByDescending(h => h.ActivityPeriod()).ToArray();
    }
  }
}