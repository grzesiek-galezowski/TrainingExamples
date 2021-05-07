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
        public void ShouldRestartAllServicesPairwise()
        {
            //GIVEN
            var timeout = Any.TimeSpan();
            using var autoMock = new AutoSubstitute(builder =>
            {
                builder.Register(context => timeout).SingleInstance();
                builder.Register(context => new List<IService>
                {
                    Substitute.For<IService>(),
                    Substitute.For<IService>(),
                    Substitute.For<IService>(),
                }).SingleInstance().Named<List<IService>>("local");
                builder.Register(context => new List<IService>
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
    }
}