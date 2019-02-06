using FluentAssertions;
using NUnit.Framework;

namespace FizzBuzzCore
{
    //TODO 2 PCs
    //TODO install fluent assertions
    //TODO install hiding of status bar
    //TODO 2 spaces instead of 4
    //TODO: explain - TDD, refactoring
    //TODO: take mouse and keyboard
    //TODO: everyone can leave anytime
    //1. Randori rules
    //2. This specific randori 2 parts
    //3. exercise (FB)
    //4. Triangulation
    //5. shortcuts (CTRL+SHIFT+R, ALT+ENTER)
    /*public class FizzBuzzAlgorithmSpecification
    {
        [TestCase(1, "1")]
        public void TODO(int input, string expected)
        {
            //GIVEN

            //WHEN

            //THEN
            Assert.Fail("Koko dzambo");
        }
    }*/

    public class FizzBuzzAlgorithmSpecification
    {
        [TestCase(1, "1")]
        [TestCase(2, "2")]
        [TestCase(3, "Fizz")]
        [TestCase(6, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(10, "Buzz")]
        [TestCase(15, "FizzBuzz")]
        public void ShouldXXXX(int input, string expected)
        {
            //GIVEN
            var fizzBuzzAlgorithm = new FizzBuzzAlgorithm();

            //WHEN
            var result = fizzBuzzAlgorithm.Translate(input);

            //THEN
            result.Should().Be(expected);
        }
    }
}
