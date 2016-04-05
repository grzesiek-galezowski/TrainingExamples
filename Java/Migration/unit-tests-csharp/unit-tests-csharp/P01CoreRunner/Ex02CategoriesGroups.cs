using NUnit.Framework;

namespace unit_tests_csharp.P01CoreRunner
{
  public class Ex02CategoriesGroups
  {
    //TODO show how resharper groups this test
    [Test, Category("SlowTest"), Category("NotReallyAUnitTests")]
    [Category("JustFoolingAround")]
    public void ShouldDoWhatever()
    {
      //GIVEN

      //WHEN

      //THEN
    }
  }
}
