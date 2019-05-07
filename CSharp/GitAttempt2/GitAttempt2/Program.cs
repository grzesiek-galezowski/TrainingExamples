using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LibGit2Sharp;

namespace GitAttempt2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var repo = new Repository(@"c:\Users\ftw637\Documents\GitHub\TrainingExamples\"))
            {
                var analysisMetadata = ScanAllCommits(repo);



                /*
                foreach (var analysisMetadata in commitsPerPath)
                {
                    if (analysisMetadata.Value._aliases.Count > 1)
                    {
                        Console.WriteLine(analysisMetadata.Key.Sha);
                        foreach (var valueAliase in analysisMetadata.Value._aliases)
                        {
                            Console.WriteLine("==>" + valueAliase);
                        }
                        Console.WriteLine(analysisMetadata.Value.ModifiedCommitsCount);
                    }
                }*/
                
                foreach (var keyValuePair in analysisMetadata.OrderBy(kvp => kvp.Value.ModifiedCommitsCount))
                {
                    Console.WriteLine(keyValuePair.Value.ModifiedCommitsCount);
                }

                PrintTreeOf(repo.Commits.Last().Tree);
            }

        }

        private static Dictionary<ObjectId, AnalysisMetadata> ScanAllCommits(Repository repo)
        {
            var commitsPerPath = new Dictionary<ObjectId, AnalysisMetadata>();
            var commits = repo.Commits.ToArray();
            for (var i = 1; i < commits.Length; ++i)
            {
                var previousCommit = commits[i - 1];
                var currentCommit = commits[i];
                var treeChanges = repo.Diff.Compare<TreeChanges>(currentCommit.Tree, previousCommit.Tree);

                foreach (var treeEntry in treeChanges)
                {
                    if (!commitsPerPath.ContainsKey(treeEntry.Oid))
                    {
                        commitsPerPath[treeEntry.Oid] = new AnalysisMetadata();
                    }

                    commitsPerPath[treeEntry.Oid].AddAlias(treeEntry.Path);
                    commitsPerPath[treeEntry.Oid].IncreaseModifiedCommittsCount();
                }
            }

            return commitsPerPath;
        }

        private static void PrintTreeOf(Tree tree)
        {
            foreach (var treeEntry in tree)
            {
                switch (treeEntry.TargetType)
                {
                    case TreeEntryTargetType.Blob:
                        Console.WriteLine(treeEntry.Path);
                        break;
                    case TreeEntryTargetType.Tree:
                        Console.WriteLine(treeEntry.Path);
                        PrintTreeOf((Tree) treeEntry.Target);
                        break;
                    case TreeEntryTargetType.GitLink:
                        throw new ArgumentException(treeEntry.Path);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    public class AnalysisMetadata
    {
        public List<string> _aliases = new List<string>();
        public int ModifiedCommitsCount { get; set; } = 0;

        public void AddAlias(string path)
        {
            _aliases.Add(path);
        }

        public void IncreaseModifiedCommittsCount()
        {
            ModifiedCommitsCount++;
        }
    }
}