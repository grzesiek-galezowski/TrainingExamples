using System;

namespace NUnitTestProject1.DI
{
    public class InjectMe3
    {
        private readonly int _initNumber;
        private readonly int _paramNumber;

        public InjectMe3(int initNumber, int paramNumber)
        {
            _initNumber = initNumber;
            _paramNumber = paramNumber;
        }

        public void DoSomethingElse()
        {
            throw new Exception($"{_initNumber}{_paramNumber}");
        }
    }
}