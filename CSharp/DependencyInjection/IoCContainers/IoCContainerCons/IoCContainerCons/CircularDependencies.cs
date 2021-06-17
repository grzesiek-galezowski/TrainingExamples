﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Autofac;
using Autofac.Core;
using NUnit.Framework;

namespace IoCContainerCons
{
    public class CircularDependencies
    {
        [Test]
        //9.3.3 Constructor/Constructor dependencies
        public void ShouldFailWhenCircularDependencyIsDiscovered()
        {
            //GIVEN
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<One>();
            containerBuilder.RegisterType<Two>();
            using var container = containerBuilder.Build();
            //WHEN
            //THEN
            //TODO uncomment to see the exception
            Assert.Throws<DependencyResolutionException>(() =>
            {
                var one = container.Resolve<One>();
            });
        }

    }

    public class One
    {
        public One(Two two)
        {
        }
    }

    public class Two
    {
        public Two(One one)
        {
        }
    }
}
