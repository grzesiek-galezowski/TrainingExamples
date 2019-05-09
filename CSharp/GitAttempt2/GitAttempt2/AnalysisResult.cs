namespace GitAttempt2
{
    public class AnalysisResult
    {
        public int ModifiedCommitsCount { get; private set; } = 0;

        public void IncreaseModifiedCommitsCount()
        {
            ModifiedCommitsCount++;
        }
    }
}