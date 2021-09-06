using NUnit.Framework;

namespace Notebook
{
    public class NullIsDifficultToTrack
    {
        [Test]
        public void ShouldShowStackTraceBeingLostWhenNullReturnIsPropagated()
        {
            int num = GetNum();
            string result = Step1(num);

            //uncomment this: Assert.NotNull(result);
        }

        private static string Step1(int num)
        {
            if (num > 120) return null;
            else return Step2(num);
        }

        private static string Step2(int num)
        {
            if (num > 110) return null;
            else return Step3(num);
        }

        private static string Step3(int num)
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