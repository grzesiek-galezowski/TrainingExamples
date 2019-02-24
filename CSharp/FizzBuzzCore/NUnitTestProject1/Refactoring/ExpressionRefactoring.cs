using NUnit.Framework;

namespace NUnitTestProject1.Refactoring
{
    public class ProductionCode
    {
        // Reduce this to a single expression using either
        // 1. a generic way
        // 2. a smart way
        public static string GetMessage()
        {
            var one = "1";
            var alpha = "alpha";
            var abString = alpha + " bravo";
            var charlie = " charlie " + one;
            var part1 = abString + charlie;
            var threeLetters = alpha+" gamma delta";
            return $"{part1} {threeLetters} {abString} beta {one}";
        }
    }
















































































































    /*
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Assert.AreEqual("alpha bravo charlie 1 alpha gamma delta alpha bravo beta 1", ProductionCode.GetMessage());
        }
    }*/

}