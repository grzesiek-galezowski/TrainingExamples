namespace GitAttempt2
{
    public class TrunkFile
    {
        public int ChangeRate { get; }
        public string Path { get; }
        public double Complexity { get; }

        public TrunkFile(int changeRate, string path, double complexity)
        {
            ChangeRate = changeRate;
            Path = path;
            Complexity = complexity;
        }
    }
}