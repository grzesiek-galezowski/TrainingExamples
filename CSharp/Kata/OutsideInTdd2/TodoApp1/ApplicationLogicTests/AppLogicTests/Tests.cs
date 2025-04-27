using ApplicationLogic.Ports;
using ApplicationLogicTests.AppLogicTests.Automation;
using TddXt.AnyRoot;

namespace ApplicationLogicTests.AppLogicTests;

public class Tests
{
  [Test]
  public async Task ShouldReportSuccessfulNodeCreation()
  {
    //GIVEN
    var noteId = Any.Guid();
    var appLogicDriver = new AppLogicDriver();
    var newTodoNoteDefinitionDto = Any.Instance<NewTodoNoteDefinitionDto>();
    appLogicDriver.SetupNextDatabaseNoteId(noteId);

    //WHEN
    var response = await appLogicDriver.AddTodoNote(newTodoNoteDefinitionDto);

    //THEN
    await response.ShouldContain(noteId);
  }

  [Test]
  public async Task ShouldAllowAccessingTheCreatedNote()
  {
    //GIVEN
    var noteId = Any.Guid();
    var appLogicDriver = new AppLogicDriver();
    var newTodoNoteDefinitionDto = Any.Instance<NewTodoNoteDefinitionDto>();
    appLogicDriver.SetupNextDatabaseNoteId(noteId);
    await appLogicDriver.AddTodoNote(newTodoNoteDefinitionDto);

    //WHEN
    var response = await appLogicDriver.RetrieveTodoNote(noteId);

    //THEN
    await response.ShouldContainNoteBasedOn(newTodoNoteDefinitionDto, noteId);
  }
  
  [Test]
  public async Task ShouldCorrectInappropriateWordsInContent()
  {
    //GIVEN
    var noteId = Any.Guid();
    var appLogicDriver = new AppLogicDriver();
    var newTodoNoteDefinitionDto = Any.Instance<NewTodoNoteDefinitionDto>()
      with { Content = "I was ran over by a truck"};
    appLogicDriver.SetupNextDatabaseNoteId(noteId);
    await appLogicDriver.AddTodoNote(newTodoNoteDefinitionDto);

    //WHEN
    var response = await appLogicDriver.RetrieveTodoNote(noteId);

    //THEN
    await response.ShouldContainNoteBasedOn(
      newTodoNoteDefinitionDto with { Content = "I was ran over by a duck"}, 
      noteId);
  }
}