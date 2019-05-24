using System;
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
            else if (!HasTheSameContent(change))
            {
                _entries.Add(change);
            }
        }

        private bool HasTheSameContent(Change change) => _entries.Last().TextWithoutWhitespaces.Equals(change.TextWithoutWhitespaces);

        public string PathOfLastVersion() => Entries.Last().Path;
        public int ChangesCount() => Entries.Count;
        public double ComplexityOfLastVersion() => Entries.Last().Complexity;
        public double HotSpotRank() => ChangesCount() * ComplexityOfLastVersion();
        public DateTimeOffset CreationDate() => Entries.First().ChangeDate;
        public DateTimeOffset LastChangeDate() => Entries.Last().ChangeDate;
        public TimeSpan ActivityPeriod() => LastChangeDate() - CreationDate();
        public TimeSpan Age() => DateTime.UtcNow - CreationDate();
    }
}