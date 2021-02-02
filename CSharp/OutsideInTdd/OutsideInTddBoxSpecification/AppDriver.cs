using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using OutsideInTdd;

namespace OutsideInTddBoxSpecification
{
    public class AppDriver : IDisposable
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;
        private readonly FlurlClient _flurlClient;

        public AppDriver()
        {
            _webApplicationFactory = new WebApplicationFactory<Startup>();
            _flurlClient = new FlurlClient(
                _webApplicationFactory.CreateDefaultClient());
        }

        public Task AddTodoNote(string title, string content)
        {
            //TODO add tests for header validation
            return _flurlClient.Request("Todo")
                .WithHeader("Content-Type", "application/json")
                .PostJsonAsync(new TodoNoteDto(title, content));
        }

        public async Task<AllRetrievedNotes> RetrieveAllNotes()
        {
            //TODO add tests for header validation
            var dto = await _flurlClient
                .Request("Todo")
                .WithHeader("Accept", "application/json")
                .GetJsonAsync<IEnumerable<TodoNoteDto>>();
            return new AllRetrievedNotes(dto);
        }

        public void Dispose()
        {
            _webApplicationFactory.Dispose();
        }
    }
}