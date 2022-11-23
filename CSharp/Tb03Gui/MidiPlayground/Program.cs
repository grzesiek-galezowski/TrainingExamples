using AtmaFileSystem;

namespace MidiPlayground;

internal class Program
{
  static async Task Main(string[] args)
  {
    using var synth = Synth.Create();
    synth.TurnOn();
    synth.SetBpm(100);

    var tb03Backup = new Tb03Backup(
      AtmaFileSystemPaths.AbsoluteDirectoryPath("C:\\Users\\HYPERBOOK\\Desktop\\TB-03-BACKUP\\"));
    var prmPattern = tb03Backup
      .Pattern(Tb03PatternGroupNumber.Group1, Tb03PatternNumberInGroup.Pattern1);

    await prmPattern.PlayOn(synth);
  }
}