using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;
using AutofacContrib.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot.Time;
using static TddXt.AnyRoot.Root;

namespace AvoidAutoMockingContainers
{
    public class _04_EdgeCaseWithAutoMock
    {
        [Test]
        public void ShouldRestartAllServicesPairwise_1()
        {
            //I could not find a dedicated API in the automocking container to address this use case.
            //Though there are at least three different ways to work around it...

            //GIVEN
            var timeout = Any.TimeSpan();
            using var autoMock = new AutoSubstitute(builder =>
            {
                builder.Register(_ => timeout).SingleInstance();
                builder.Register(_ => new List<IService>
                {
                    Substitute.For<IService>(),
                    Substitute.For<IService>(),
                    Substitute.For<IService>(),
                }).SingleInstance().Named<List<IService>>("local");
                builder.Register(_ => new List<IService>
                {
                    Substitute.For<IService>(),
                    Substitute.For<IService>(),
                    Substitute.For<IService>(),
                }).SingleInstance().Named<List<IService>>("remote");
                builder.Register(context => new RestartSequences(
                    context.ResolveNamed<List<IService>>("local"),
                    context.ResolveNamed<List<IService>>("remote"),
                    context.Resolve<TimeSpan>()
                    ));
            });
            var sequences = autoMock.Resolve<RestartSequences>();

            //WHEN
            sequences.RestartAll();

            //THEN
            Received.InOrder(() =>
            {
                autoMock.Container.ResolveNamed<List<IService>>("local")[0].Restart(timeout);
                autoMock.Container.ResolveNamed<List<IService>>("remote")[0].Restart(timeout);
                autoMock.Container.ResolveNamed<List<IService>>("local")[1].Restart(timeout);
                autoMock.Container.ResolveNamed<List<IService>>("remote")[1].Restart(timeout);
                autoMock.Container.ResolveNamed<List<IService>>("local")[2].Restart(timeout);
                autoMock.Container.ResolveNamed<List<IService>>("remote")[2].Restart(timeout);
            });
        }

        [Test] //we can use automocker like this, but... ;-)
        public void ShouldRestartAllServicesPairwise_2()
        {
            //GIVEN
            using var autoMock = new AutoSubstitute();
            var localService1 = Substitute.For<IService>();
            var localService2 = Substitute.For<IService>();
            var localService3 = Substitute.For<IService>();
            var remoteService1 = Substitute.For<IService>();
            var remoteService2 = Substitute.For<IService>();
            var remoteService3 = Substitute.For<IService>();
            var timeout = Any.TimeSpan();
            var sequences = autoMock.Resolve<RestartSequences>(
                new PositionalParameter(0, new List<IService> {localService1, localService2, localService3}),
                new PositionalParameter(1, new List<IService> {remoteService1, remoteService2, remoteService3}),
                new PositionalParameter(2, timeout));

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