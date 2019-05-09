using System;
using System.Collections.Generic;
using System.Linq;
using GitAttempt2;
using LibGit2Sharp;

static internal class RepoAnalysis
{
    public static IEnumerable<TrunkFile> Analyze(string repositoryPath, string branchName)
    {
        //using var repo = new Repository(@"c:\Users\ftw637\Documents\GitHub\TrainingExamples\");
        //using var repo = new Repository(@"C:\Users\grzes\Documents\GitHub\nscan\");

        using var repo = new Repository(repositoryPath);
        var commits = repo.Branches[branchName].Commits.Reverse();
        var analysisMetadata = CalculateChangeRates(repo, commits.ToArray());

        var pathsInTrunk = new List<string>();
        CollectPathsFrom(commits.Last().Tree, pathsInTrunk);

        var trunkFiles = Enumerable.Where(analysisMetadata, am => pathsInTrunk.Contains(am.Key))
            .Select(x => new TrunkFile(x.Value, x.Key));
        return trunkFiles;
    }

    private static Dictionary<string, AnalysisResult> CalculateChangeRates(Repository repo, IReadOnlyList<Commit> commits)
    {
        var analysisPerPath = new Dictionary<string, AnalysisResult>();

        AddCommit(commits.First().Tree, analysisPerPath);
        for (var i = 1; i < commits.Count; ++i)
        {
            var previousCommit = commits[i - 1];
            var currentCommit = commits[i];

            AnalyzeChanges(repo.Diff.Compare<TreeChanges>(previousCommit.Tree, currentCommit.Tree), analysisPerPath);
        }

        return analysisPerPath;
    }

    private static void AddCommit(Tree treeEntries, Dictionary<string, AnalysisResult> commitsPerPath)
    {
        foreach (var treeEntry in treeEntries)
        {
            switch (treeEntry.TargetType)
            {
                case TreeEntryTargetType.Blob:
                    if (!commitsPerPath.ContainsKey(treeEntry.Path))
                    {
                        commitsPerPath[treeEntry.Path] = new AnalysisResult();
                    }
                    commitsPerPath[treeEntry.Path].IncreaseModifiedCommitsCount();

                    break;
                case TreeEntryTargetType.Tree:
                    AddCommit((Tree) treeEntry.Target, commitsPerPath);
                    break;
                case TreeEntryTargetType.GitLink:
                    throw new ArgumentException(treeEntry.Path);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }

    private static void AnalyzeChanges(TreeChanges treeChanges, Dictionary<string, AnalysisResult> analysisResultPerPath)
    {
        foreach (var treeEntry in treeChanges)
        {
            switch (treeEntry.Status)
            {
                case ChangeKind.Unmodified:
                    break;
                case ChangeKind.Added:
                    analysisResultPerPath[treeEntry.Path] = new AnalysisResult();
                    analysisResultPerPath[treeEntry.Path].IncreaseModifiedCommitsCount();
                    break;
                case ChangeKind.Deleted:
                    break;
                case ChangeKind.Modified:
                    analysisResultPerPath[treeEntry.Path].IncreaseModifiedCommitsCount();
                    break;
                case ChangeKind.Renamed:
                    analysisResultPerPath[treeEntry.Path] = analysisResultPerPath[treeEntry.OldPath];
                    analysisResultPerPath[treeEntry.Path].IncreaseModifiedCommitsCount();
                    break;
                case ChangeKind.Copied:
                    analysisResultPerPath[treeEntry.Path] = new AnalysisResult();
                    analysisResultPerPath[treeEntry.Path].IncreaseModifiedCommitsCount();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private static void CollectPathsFrom(Tree tree, List<string> pathsByOid)
    {
        foreach (var treeEntry in tree)
        {
            switch (treeEntry.TargetType)
            {
                case TreeEntryTargetType.Blob:
                    pathsByOid.Add(treeEntry.Path);
                    break;
                case TreeEntryTargetType.Tree:
                    CollectPathsFrom((Tree) treeEntry.Target, pathsByOid);
                    break;
                case TreeEntryTargetType.GitLink:
                    throw new ArgumentException(treeEntry.Path);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}