using System;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using OutsideInTdd.Adapters;
using OutsideInTdd.App;
using TddXt.AnyRoot.Strings;
using static TddXt.AnyRoot.Root;

namespace OutsideInTddBoxSpecification.Integration
{
    public class EndpointIntegrationDriver : IDisposable
    {
        private TodoNoteDto _todoNoteDto;
        private ITodoCommandFactory _todoCommandFactory;
        private ITodoCommand _command;
        private IHost _host;
        private FlurlClient _client;
        private IFlurlResponse _response;
        private readonly string _inappropriateWord = Any.String();
        private IAddTodoResponseInProgress _responseInProgress;

        public async Task InitializeAsync()
        {
            _todoNoteDto = Any.Instance<TodoNoteDto>();
            _todoCommandFactory = Substitute.For<ITodoCommandFactory>();
            _host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder =>
                    builder
                        .UseStartup<Startup>()
                        .ConfigureServices(collection =>
                        {
                            collection.RemoveAll<EndpointsRoot>();
                            collection.AddSingleton(_ => new EndpointsRoot(_todoCommandFactory,
                                Any.Instance<ITodoNoteDao>()));
                        })
                        .UseTestServer()).Build();
            await _host.StartAsync();
            _client = new FlurlClient(_host.GetTestServer().CreateClient());
        }

        public void SetupSuccessfulAddTodoCommand()
        {
            _command = Substitute.For<ITodoCommand>();
            _todoCommandFactory.CreateAddNoteCommand(_todoNoteDto, Arg.Any<IAddTodoResponseInProgress>())
                .Returns(_command);
        }

        public void SetupAddTodoCommandFailedBecauseOfInappropriateWord()
        {
            _command = Substitute.For<ITodoCommand>();
            _command
                .When(c => c.Execute())
                .Do(_ => _responseInProgress.ReportFailureBecauseOfInappropriateWord(_inappropriateWord));
            _todoCommandFactory.CreateAddNoteCommand(_todoNoteDto, Arg.Any<IAddTodoResponseInProgress>()).Returns(
                info =>
                {
                    _responseInProgress = info.Arg<IAddTodoResponseInProgress>();
                    return _command;
                });
        }

        public async Task InvokeAddTodoEndpoint()
        {
            _response = await _client.Request("Todo").AllowAnyHttpStatus()
                .PostJsonAsync(_todoNoteDto);
        }

        public void ResponseShouldBeOk()
        {
            _response.StatusCode.Should().Be(200);
        }

        public void Dispose()
        {
            _host.Dispose();
        }


        public void ResponseShouldBeInternalServerErrorWithBadWordMessage()
        {
            _response.StatusCode.Should().Be(400);
            _response.ResponseMessage.Content.ReadAsStringAsync().Result.Should().Be(_inappropriateWord);
        }
    }
}