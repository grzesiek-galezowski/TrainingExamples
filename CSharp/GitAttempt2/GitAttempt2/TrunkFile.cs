namespace GitAttempt2
{
    public class TrunkFile
    {
        public AnalysisResult Result { get; }
        public string Path { get; }

        public TrunkFile(AnalysisResult result, string path)
        {
            Result = result;
            Path = path;
        }
    }
}