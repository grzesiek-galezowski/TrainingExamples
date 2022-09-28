using FluentAssertions;
using Flurl.Http;
using TodoApp1Tests.TestDtos;

namespace TodoApp1Tests.HostTests.Automation;

public class AttemptToCreateTodoNoteResponse
{
    private readonly IFlurlResponse _flurlResponse;

    public AttemptToCreateTodoNoteResponse(IFlurlResponse flurlResponse)
    {
        _flurlResponse = flurlResponse;
    }

    public void ShouldBeSuccessful()
    {
        _flurlResponse.StatusCode.Should().Be(200);
    }

    public async Task ShouldContainValidId()
    {
        var metadata = await DeserializeDto();
        metadata.Id.Should().NotBeEmpty();
    }

    private Task<TodoNoteMetadataTestDto> DeserializeDto()
    {
      return _flurlResponse.GetJsonAsync<TodoNoteMetadataTestDto>();
    }
}