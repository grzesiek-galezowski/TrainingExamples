using NUnit.Framework;

namespace FizzBuzz
{
  class FizzBuzzSpecification
  {
    [Test]
    public void ShouldXXXXXYYYYYYZZZZZ()
    {
      //GIVEN
      var algorithm = new FizzBuzzAlgorithm();

      //WHEN
      var result = algorithm.Convert(1);

      //THEN
      Assert.AreEqual("1", result);
    }
  }
}
