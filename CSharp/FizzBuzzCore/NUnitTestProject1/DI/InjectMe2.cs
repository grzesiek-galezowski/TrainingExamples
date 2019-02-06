using System;

namespace NUnitTestProject1.DI
{
    public class InjectMe2
    {
        private readonly int _i;

        public InjectMe2(int i)
        {
            _i = i;
        }

        public void DoSomethingElse()
        {
            throw new Exception(_i.ToString());
        }
    }
}