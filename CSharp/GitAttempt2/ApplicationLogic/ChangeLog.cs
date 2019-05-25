using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationLogic
{
    public class ChangeLog
    {
        private readonly List<Change> _entries = new List<Change>();
        private int _complexityRank = -1; //bug handle another way
        private int _changeCountRank = -1; //bug handle another way

        public IReadOnlyList<Change> Entries => _entries;

        public void AddDataFrom(Change change)
        {
            if (!_entries.Any())
            {
                _entries.Add(change);
            }
            else if (!HasTheSameContent(change))
            {
                _entries.Add(change);
            }
        }

        private bool HasTheSameContent(Change change) => _entries.Last().TextWithoutWhitespaces.Equals(change.TextWithoutWhitespaces);

        public string PathOfCurrentVersion() => Entries.Last().Path;
        public int ChangesCount() => Entries.Count;
        public double ComplexityOfCurrentVersion() => Entries.Last().Complexity;
        public double HotSpotRank() => (_changeCountRank + _complexityRank) / 3d; //ChangesCount() * ComplexityOfCurrentVersion();
        public DateTimeOffset CreationDate() => Entries.First().ChangeDate;
        public DateTimeOffset LastChangeDate() => Entries.Last().ChangeDate;
        public TimeSpan ActivityPeriod() => LastChangeDate() - CreationDate();
        public TimeSpan Age() => DateTime.UtcNow - CreationDate();
        public TimeSpan TimeSinceLastChange() => DateTime.UtcNow - LastChangeDate();

        public void AssignComplexityRank(int complexityRank)
        {
          _complexityRank = complexityRank;
        }

        public void AssignChangeCountRank(int changeCountRank)
        {
          _changeCountRank = changeCountRank;
        }

        public void AssignActivityPeriodRank()
        {
        }
    }
}