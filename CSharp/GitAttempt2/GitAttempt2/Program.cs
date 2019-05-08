using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using LibGit2Sharp;

namespace GitAttempt2
{
    public class Map<T1, T2>
    {
        private Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
        private Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();

        public Map()
        {
            this.Forward = new Indexer<T1, T2>(_forward);
            this.Reverse = new Indexer<T2, T1>(_reverse);
        }

        public class Indexer<T3, T4>
        {
            private Dictionary<T3, T4> _dictionary;
            public Indexer(Dictionary<T3, T4> dictionary)
            {
                _dictionary = dictionary;
            }
            public T4 this[T3 index]
            {
                get { return _dictionary[index]; }
                set { _dictionary[index] = value; }
            }
        }

        public void Add(T1 t1, T2 t2)
        {
            _forward.Add(t1, t2);
            _reverse.Add(t2, t1);
        }

        public Indexer<T1, T2> Forward { get; private set; }
        public Indexer<T2, T1> Reverse { get; private set; }
    }

    //bug use own ids instead of oids or paths. Both are not reliable
    class Program
    {
        private Map<string, ObjectId> oidToPath

        static void Main(string[] args)
        {
            //using var repo = new Repository(@"c:\Users\ftw637\Documents\GitHub\TrainingExamples\");
            using var repo = new Repository(@"C:\Users\grzes\Documents\GitHub\TrainingExamples\");
            var analysisMetadata = ScanAllCommits(repo);

            var pathsByOid = new Dictionary<ObjectId, string>();
            PrintTreeOf(repo.Branches["master"].Commits.Reverse().Last().Tree, pathsByOid);

            var trunkFiles = analysisMetadata.Where(am => pathsByOid.ContainsKey(am.Key)).Select(x => new TrunkFile(x.Key, x.Value, pathsByOid[x.Key]));

            foreach (var trunkFile in trunkFiles.OrderBy(f => f._metadata.ModifiedCommitsCount))
            {
                if (trunkFile._path.Contains("Controller"))
                {
                    Console.WriteLine(trunkFile._path + " => " + trunkFile._metadata.ModifiedCommitsCount);
                }
            }
        }

        private static Dictionary<ObjectId, AnalysisMetadata> ScanAllCommits(Repository repo)
        {
            var commitsPerPath = new Dictionary<ObjectId, AnalysisMetadata>();
            var commits = repo.Branches["master"].Commits.Reverse().ToArray();

            Commit first = commits.First();
            AddCommit(first.Tree, commitsPerPath);
            for (var i = 1; i < commits.Length; ++i)
            {
                var previousCommit = commits[i - 1];
                var currentCommit = commits[i];

                AddFiles(repo.Diff.Compare<TreeChanges>(previousCommit.Tree, currentCommit.Tree), commitsPerPath);
            }

            return commitsPerPath;
        }

        private static void AddCommit(Tree treeEntries, IDictionary<ObjectId, AnalysisMetadata> commitsPerPath)
        {
            foreach (var treeEntry in treeEntries)
            {
                switch (treeEntry.TargetType)
                {
                    case TreeEntryTargetType.Blob:
                        if (!commitsPerPath.ContainsKey(treeEntry.Target.Id))
                        {
                            commitsPerPath[treeEntry.Target.Id] = new AnalysisMetadata();
                        }

                        commitsPerPath[treeEntry.Target.Id].AddAlias(treeEntry.Path);
                        commitsPerPath[treeEntry.Target.Id].IncreaseModifiedCommittsCount();

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

        private static void AddFiles(TreeChanges treeChanges, Dictionary<ObjectId, AnalysisMetadata> commitsPerPath)
        {
            foreach (var treeEntry in treeChanges)
            {
                if (!commitsPerPath.ContainsKey(treeEntry.Oid))
                {
                    if (treeEntry.Path.Contains("UsersController.cs"))
                    {
                        Console.WriteLine("YESSS " + treeEntry.Path + " " + treeEntry.Oid);
                    }

                    commitsPerPath[treeEntry.Oid] = new AnalysisMetadata();
                }

                commitsPerPath[treeEntry.Oid].AddAlias(treeEntry.Path);
                commitsPerPath[treeEntry.Oid].IncreaseModifiedCommittsCount();
            }
        }

        private static void PrintTreeOf(Tree tree, Dictionary<ObjectId, string> pathsByOid)
        {
            foreach (var treeEntry in tree)
            {
                switch (treeEntry.TargetType)
                {
                    case TreeEntryTargetType.Blob:
                        pathsByOid[treeEntry.Target.Id] = treeEntry.Path;
                        break;
                    case TreeEntryTargetType.Tree:
                        PrintTreeOf((Tree) treeEntry.Target, pathsByOid);
                        break;
                    case TreeEntryTargetType.GitLink:
                        throw new ArgumentException(treeEntry.Path);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    public class TrunkFile
    {
        public readonly ObjectId _id;
        public readonly AnalysisMetadata _metadata;
        public readonly string _path;

        public TrunkFile(ObjectId id, AnalysisMetadata metadata, string path)
        {
            _id = id;
            _metadata = metadata;
            _path = path;
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