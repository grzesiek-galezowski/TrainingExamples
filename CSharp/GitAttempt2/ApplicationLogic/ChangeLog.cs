using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        private bool HasTheSameContent(Change change)
        {
            return _entries.Last().TextWithoutWhitespaces.Equals(change.TextWithoutWhitespaces);
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