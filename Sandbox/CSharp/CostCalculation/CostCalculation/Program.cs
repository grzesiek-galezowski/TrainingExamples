using AtmaFileSystem;
using AtmaFileSystem.IO;
using SimpleExec;

AbsoluteDirectoryPath.Value("C:\\Users\\HYPERBOOK\\Documents\\GitHub\\legacy-fighter-temp\\Main").SetAsCurrentDirectory();
var linesInCsProjs = await LinesInCsProjs("csproj");
var linesInCsFiles = await LinesInCsProjs("cs");
var originCodeBaseSize = linesInCsProjs + linesInCsFiles;
var dealBase = originCodeBaseSize * 2;

AbsoluteDirectoryPath.Value("C:\\Users\\HYPERBOOK\\Documents\\GitHub\\cabs-csharp").SetAsCurrentDirectory();

var numberOfCommits = await GetNumberOfCommits();
var stats = await GetStatsPerCommitt(numberOfCommits);

const string insertions = "insertions(+)";
const string insertion = "insertion(+)";
const string deletions = "deletions(-)";
const string deletion = "deletion(-)";
const string modifications = "modifications(!)";
const string modification = "modification(!)";
var statPerType = new Dictionary<string, int>()
{
  [insertions] = 0,
  [insertion] = 0,
  [deletions] = 0,
  [deletion] = 0,
  [modifications] = 0,
  [modification] = 0
};

foreach (var stat in stats)
{
  Console.WriteLine(stat);
  var stringPartPerStat = stat.Split(", ", StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray();
  Console.WriteLine(string.Join(" == ", stringPartPerStat));
  foreach (var singleStat in stringPartPerStat)
  {
    Update(statPerType, singleStat);
  }
}

Console.WriteLine($"Original code - lines in csproj files: {linesInCsProjs}");
Console.WriteLine($"Original code - lines in cs files: {linesInCsFiles}");
Console.WriteLine($"Original code - total lines: {originCodeBaseSize}");
Console.WriteLine($"Deal base lines:  {originCodeBaseSize}*2 = {dealBase}");
Console.WriteLine($"{insertions} {statPerType[insertions] + statPerType[insertion]}");
Console.WriteLine($"{modifications} {statPerType[modifications] + statPerType[modification]}");
Console.WriteLine($"{deletions} {statPerType[deletions] + statPerType[deletion]}");
Console.WriteLine($"Sum of changes: {statPerType.Values.Sum()}");
var totalChangesSize = statPerType.Values.Sum() + originCodeBaseSize;
Console.WriteLine($"Total changes size: {totalChangesSize}");
Console.WriteLine($"Ratio compared to deal base: {totalChangesSize}/{dealBase} = {totalChangesSize/(double)dealBase}");

async Task<int> LinesInCsProjs(string extension)
{
  return int.Parse((await Command.ReadAsync("wsl", $"git ls-files | grep __GOLD | grep .{extension} | xargs cat | wc -l")).StandardOutput);
}

async Task<int> GetNumberOfCommits()
{
  return (await Command.ReadAsync("wsl", "git reflog")).StandardOutput
    .Split("\n", StringSplitOptions.RemoveEmptyEntries).Length;
}

async Task<string> GetStatsForCommit(int positionRelativeToHead)
{
  var output = await Command.ReadAsync(
    "wsl", "git diff --patience -M " +
           $"HEAD@{{{positionRelativeToHead}}} HEAD@{{{positionRelativeToHead - 1}}}" +
           " | diffstat -Cm");
  return output.StandardOutput.Split("\n", StringSplitOptions.RemoveEmptyEntries).Last();
}

async Task<string[]> GetStatsPerCommitt(int numberOfCommits1)
{
  var statTasks = new List<Task<string>>();
  for (int i = numberOfCommits1; i >= 1; i--)
  {
    statTasks.Add(GetStatsForCommit(i));
  }

  var strings = await Task.WhenAll(statTasks);
  return strings;
}

void Update(IDictionary<string, int> dictionary, string s)
{
  var numberAndHeaderString = s.Split(" ", StringSplitOptions.RemoveEmptyEntries);
  var statType = numberAndHeaderString[1];
  var statValue = int.Parse(numberAndHeaderString[0]);
  dictionary[statType] += statValue;
}