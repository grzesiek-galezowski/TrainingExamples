using NUnit.Framework;

namespace Notebook
{
    public class NullIsDifficultToTrack
    {
        [Test]
        public void ShouldShowStackTraceBeingLostWhenNullReturnIsPropagated()
        {
            int num = GetNum();
            string result = A(num);

            Assert.NotNull(result);
        }

        private static string A(int num)
        {
            if (num > 120) return null;
            else return B(num);
        }

        private static string B(int num)
        {
            if (num > 110) return null;
            else return C(num);
        }

        private static string C(int num)
        {
            if (num > 100) return null;
            else return num.ToString();
        }

        private static int GetNum()
        {
            return 101;
        }
    }
}