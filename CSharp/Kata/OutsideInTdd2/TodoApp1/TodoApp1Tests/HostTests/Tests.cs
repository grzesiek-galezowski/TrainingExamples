using TodoApp1Tests.HostTests.Automation;
using TodoApp1Tests.TestDtos;

namespace TodoApp1Tests.HostTests;

public class Tests
{
    [Test]
    public async Task ShouldReportSuccessfulNoteCreation()
    {
        //GIVEN
        await using var appDriver = AppDriver.CreateInstance();
        appDriver.Start();

        //WHEN
        var result = await appDriver.AttemptToCreateATodoNote(
          Any.Instance<NewTodoNoteDefinitionTestDto>());

        //THEN
        result.ShouldBeSuccessful();
        await result.ShouldContainValidId();
    }

    [Test]
    public async Task ShouldAllowAccessingTheCreatedNote()
    {
        //GIVEN
        var definitionDto = Any.Instance<NewTodoNoteDefinitionTestDto>();
        await using var appDriver = AppDriver.CreateInstance();
        appDriver.Start();

        var creationResult = await appDriver.CreateATodoNote(definitionDto);

        //WHEN
        var retrievalResult = await appDriver.RetrieveTodoNote(creationResult.Id);

        //THEN
        retrievalResult.ShouldContainNoteCreatedFrom(definitionDto, creationResult.Id);
    }
}