using System;
using System.Linq;

namespace GitAttempt2
{
    public class Program
    {
        static void Main(string[] args)
        {
            var trunkFiles = RepoAnalysis.Analyze(@"c:\Users\reporepo", "master");

            foreach (var trunkFile in trunkFiles.OrderBy(f => f.Result.ModifiedCommitsCount))
            {
                Console.WriteLine(trunkFile.Path + " => " + trunkFile.Result.ModifiedCommitsCount);
            }
        }
    }
}