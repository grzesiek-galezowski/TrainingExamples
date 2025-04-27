using System;

namespace NUnitTestProject1.DI
{
    public class InjectMe1
    {
        private readonly int _i;

        public InjectMe1(int i)
        {
            _i = i;
        }

        public void DoSomethingElse()
        {
            throw new Exception(_i.ToString());   
        }
    }
}