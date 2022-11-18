using FluentAssertions;
using Flurl.Http;
using TodoApp1Tests.TestDtos;

namespace TodoApp1Tests.AdapterTests.Endpoints.Automation;

public class AddTodoItemResponse
{
    private readonly IFlurlResponse _response;

    public AddTodoItemResponse(IFlurlResponse response)
    {
        _response = response;
    }

    public void ShouldBeSuccessful()
    {
        _response.StatusCode.Should().Be(200);
    }

    public async Task ShouldContainFilledMetadata()
    {
        var todoNoteMetadataTestDto = await _response.GetJsonAsync<TodoNoteMetadataTestDto>();
        todoNoteMetadataTestDto.Should().NotBeNull();
        todoNoteMetadataTestDto.Id.Should().NotBeEmpty();
    }

    public void ShouldBeBadRequest()
    {
      _response.StatusCode.Should().Be(400);
    }
}