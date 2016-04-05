using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkit.NSubstitute;

namespace unit_tests_csharp.P04NSubstitute
{
  public class Ex05ZeroInteractions
  {
    [Test]
    public void ShouldDoNothingWhenExecuted()
    {
      //GIVEN
      var command = new NullCommand();

      //WHEN
      var core = Substitute.For<ISharedCore>();
      command.ExecuteOn(core);

      //THEN
      core.ReceivedNothing();
    }
  }
}