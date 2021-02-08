using NUnit.Framework;
using TddXt.AnyRoot.Strings;
using static TddXt.AnyRoot.Root;

namespace XUnitTestPatterns._07_PickingTestValues
{
  public class _04_DerivedValues
  {
    [Test]
    public void ShouldCreateBackupFileNameContainingPassedHostNameAndUserName()
    {
      //GIVEN
      var hostName = Any.String();
      var userName = Any.String();
      var backupNamePattern = new BackupFileNamePattern();

      //WHEN
      var name = backupNamePattern.ApplyTo(hostName, userName);

      //THEN
      Assert.AreEqual($"backup_{hostName}_{userName}.zip", name);
    }
    //todo show extracting helper method
  }
}