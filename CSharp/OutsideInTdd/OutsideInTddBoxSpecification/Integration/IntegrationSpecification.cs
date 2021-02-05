using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using NUnit.Framework;
using OutsideInTdd.Adapters;
using OutsideInTdd.App;
using TddXt.AnyRoot;

namespace OutsideInTddBoxSpecification.Integration
{
    public class IntegrationSpecification
    {
        [Test]
        public async Task ShouldNAME() //bug
        {
            //GIVEN
            using var host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder =>
                    builder
                        .UseStartup<Startup>()
                        .ConfigureServices(collection =>
                        {
                            collection.RemoveAll<EndpointsRoot>();
                            collection.AddSingleton(ctx => new EndpointsRoot(
                                Substitute.For<TodoCommandFactory>(),
                                Root.Any.Instance<ITodoNoteDao>()));
                        })
                        .UseTestServer()).Build();
            await host.StartAsync();

            //opakowujemy klientem Flurla HttpClienta którego daje nam TestServer!
            using var client = new FlurlClient(host.GetTestServer().CreateClient());
            //bug finish
            
            //WHEN

            //THEN
            true.Should().BeFalse("not implemented");
        }
    }
}
