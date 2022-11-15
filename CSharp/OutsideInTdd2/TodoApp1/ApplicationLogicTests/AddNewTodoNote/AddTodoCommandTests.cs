using ApplicationLogic.AddNewTodoNote;
using ApplicationLogic.Ports;
using NSubstitute;
using TddXt.AnyRoot.Invokable;

namespace ApplicationLogicTests.AddNewTodoNote;

public class AddTodoCommandTests
{
  [Test]
  public async Task ShouldCorrectAndPersistNoteDefinitionWhenExecuted()
  {
    //GIVEN
    var dao = Any.Instance<ITodoNoteDao>();
    var afterPersistenceSteps = Any.Instance<IAfterTodoNotePersistenceSteps>();
    var definition = Substitute.For<ITodoNoteDefinition>();
    var command = new AddTodoCommand(dao, afterPersistenceSteps, definition);
    var cancellationToken = Any.CancellationToken();

    //WHEN
    await command.Execute(cancellationToken);

    //THEN
    Received.InOrder(() =>
    {
      definition.Correct();
      definition.PersistIn(dao, afterPersistenceSteps, cancellationToken);
    });
  }
}