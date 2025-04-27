using System;
using FluentAssertions;
using NUnit.Framework;

namespace NUnitTestProject1.DI
{
    public class ThreeFormsOfInjection
    {
        [Test]
        public void Whatever()
        {
            Assert.Throws<Exception>(() => 
                    new ObjectWithDependency1(1).DoSomething())
                .Message.Should().Be("1");
            Assert.Throws<Exception>(() => 
                    new ObjectWithDependency2().DoSomething(2))
                .Message.Should().Be("2");
            Assert.Throws<Exception>(() => 
                    new ObjectWithDependency3(1).DoSomething(2))
                .Message.Should().Be("12");
        }
    }

    public class ObjectWithDependency3
    {
        private readonly int _initNumber;

        public ObjectWithDependency3(int initNumber)
        {
            _initNumber = initNumber;
        }

        public void DoSomething(int paramNumber)
        {
            new InjectMe3(_initNumber, paramNumber).DoSomethingElse();
        }
    }

    public class ObjectWithDependency2
    {
        public void DoSomething(int i)
        {
            new InjectMe2(i).DoSomethingElse();
        }
    }

    public class ObjectWithDependency1
    {
        private readonly int _i;

        public ObjectWithDependency1(int i)
        {
            _i = i;
        }

        public void DoSomething()
        {
            new InjectMe1(_i).DoSomethingElse();
        }
    }
}
