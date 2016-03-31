using NUnit.Framework;
using TddEbook.TddToolkit;

namespace unit_tests_csharp.P03Any
{
  public class Ex02GenericAny
  {
    [Test]
    public void ShouldCreateGenericInstances()
    {
      //WHEN
      MyClass<int> anonymous = Any.Instance<MyClass<int>>();

      //THEN
      Assert.AreNotEqual(2, anonymous.Instance); //TODO change to AreEqual()
    }
  }
}