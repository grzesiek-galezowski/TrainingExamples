using System.Collections.Generic;

namespace GitAttempt2
{
    public class HistoryAnalysisResult
    {
        public int ChangeRate { get; }
        public IReadOnlyList<Change> History { get; }
        public string Path { get; }

        public HistoryAnalysisResult(string path, IReadOnlyList<Change> history)
        {
            ChangeRate = history.Count;
            History = history;
            Path = path;
        }
    }
}