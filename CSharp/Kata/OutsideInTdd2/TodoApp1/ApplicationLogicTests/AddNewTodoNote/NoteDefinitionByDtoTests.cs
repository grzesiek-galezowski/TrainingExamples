using ApplicationLogic.AddNewTodoNote;
using ApplicationLogic.Ports;
using NSubstitute;
using TddXt.AnyRoot.Invokable;
using TddXt.AnyRoot.Strings;

namespace ApplicationLogicTests.AddNewTodoNote;

public class NoteDefinitionByDtoTests
{
  [Test]
  public async Task ShouldSaveNoteWithTheWordTruckChangedToDuckWhenAskedToCorrectItself()
  {
    //GIVEN
    var inappropriateContent = Any.StringContaining("truck");
    var dto = Any.Instance<NewTodoNoteDefinitionDto>() with
    {
      Content = inappropriateContent
    };
    var definition = new NoteDefinitionByDto(dto);
    var dao = Substitute.For<ITodoNoteDao>();
    var steps = Any.Instance<IAfterTodoNotePersistenceSteps>();
    var cancellationToken = Any.CancellationToken();
    
    definition.Correct();

    //WHEN
    await definition.PersistIn(dao, steps, cancellationToken);

    //THEN
    await dao.Received(1)
      .Save(
        dto with
        {
          Content = inappropriateContent.Replace("truck", "duck")
        }, cancellationToken);
  }
  
  [Test]
  public async Task ShouldExecuteNextStepsWithTheResultOfNotePersistence()
  {
    //GIVEN
    var dto = Any.Instance<NewTodoNoteDefinitionDto>();
    var definition = new NoteDefinitionByDto(dto);
    var dao = Substitute.For<ITodoNoteDao>();
    var steps = Substitute.For<IAfterTodoNotePersistenceSteps>();
    var cancellationToken = Any.CancellationToken();
    var metadata = Any.Instance<TodoNoteMetadataDto>();

    dao.Save(dto, cancellationToken).Returns(metadata);

    //WHEN
    await definition.PersistIn(dao, steps, cancellationToken);

    //THEN
    await steps.Received(1).ExecuteFor(metadata, cancellationToken);
  }
}