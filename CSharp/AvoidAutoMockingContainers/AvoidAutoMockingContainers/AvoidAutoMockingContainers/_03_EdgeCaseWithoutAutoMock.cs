using System.Collections;
using Autofac.Core;
using AutofacContrib.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Time;
using static TddXt.AnyRoot.Root;

namespace AvoidAutoMockingContainers
{
    public class _03_EdgeCaseWithoutAutoMock
    {
        [Test]
        public void ShouldRestartAllServicesPairwise()
        {
            //GIVEN
            var localService1 = Substitute.For<IService>();
            var localService2 = Substitute.For<IService>();
            var localService3 = Substitute.For<IService>();
            var remoteService1 = Substitute.For<IService>();
            var remoteService2 = Substitute.For<IService>();
            var remoteService3 = Substitute.For<IService>();
            var timeout = Any.TimeSpan();
            var sequences = new RestartSequences(
                new() {localService1, localService2, localService3}, 
                new() {remoteService1, remoteService2, remoteService3}, 
                timeout);

            //WHEN
            sequences.RestartAll();

            //THEN
            Received.InOrder(() =>
            {
                localService1.Restart(timeout);
                remoteService1.Restart(timeout);
                localService2.Restart(timeout);
                remoteService2.Restart(timeout);
                localService3.Restart(timeout);
                remoteService3.Restart(timeout);
            });
        }
    }
}