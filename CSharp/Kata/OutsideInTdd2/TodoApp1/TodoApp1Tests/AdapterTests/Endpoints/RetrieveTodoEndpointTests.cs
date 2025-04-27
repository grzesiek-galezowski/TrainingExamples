using TddXt.AnyRoot;
using TodoApp1Tests.AdapterTests.Endpoints.Automation;
using TodoApp1Tests.TestDtos;

namespace TodoApp1Tests.AdapterTests.Endpoints;

public class RetrieveTodoEndpointTests
{
  [Test]
  public async Task ShouldRespondWithSuccessWhenCommandReportsASuccessToRetrievingATodoItem()
  {
    //GIVEN
    await using var driver = new EndpointsAdapterDriver();
    var id = Any.Guid();
    var returnedDto = Any.Instance<TodoNoteTestDto>();

    driver.RetrievingNoteFromAppLogicReturns(returnedDto);

    //WHEN
    var retrieveTodoItemResponse = await driver.AttemptToRetrieveATodoItem(id);

    //THEN
    retrieveTodoItemResponse.ShouldBeSuccessful();
    await retrieveTodoItemResponse.ShouldContain(returnedDto);
  }
}