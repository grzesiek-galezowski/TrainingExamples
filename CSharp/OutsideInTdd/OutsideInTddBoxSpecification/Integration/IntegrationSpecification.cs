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
using static TddXt.AnyRoot.Root;

namespace OutsideInTddBoxSpecification.Integration
{
    public class IntegrationSpecification
    {
        [Test]
        public async Task ShouldNAME() //bug
        {
            //GIVEN
            var todoNoteDto = Any.Instance<TodoNoteDto>();
            var todoCommandFactory = Substitute.For<ITodoCommandFactory>();
            using var host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder =>
                    builder
                        .UseStartup<Startup>()
                        .ConfigureServices(collection =>
                        {
                            collection.RemoveAll<EndpointsRoot>();
                            collection.AddSingleton(ctx => new EndpointsRoot(
                                todoCommandFactory,
                                Any.Instance<ITodoNoteDao>()));
                        })
                        .UseTestServer()).Build();
            await host.StartAsync();

            //bug extract interface from command
            todoCommandFactory.CreateAddNoteCommand(todoNoteDto, Arg.Any<IAddTodoResponse>())
                .Returns(command);

            //opakowujemy klientem Flurla HttpClienta którego daje nam TestServer!
            using var client = new FlurlClient(host.GetTestServer().CreateClient());
            //bug finish
            
            //WHEN
            await client.Request("Todo").PostJsonAsync(todoNoteDto);

            //THEN
            true.Should().BeFalse("not implemented");
        }
    }
}
