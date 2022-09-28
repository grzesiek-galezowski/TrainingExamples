using ApplicationLogic.Ports;

namespace ApplicationLogic;

public class ApplicationLogicRoot
{
    public ApplicationLogicRoot(ITodoNoteDao todoNoteDao)
    {
        TodoCommandFactory = new TodoCommandFactory(todoNoteDao);
    }

    public ITodoCommandFactory TodoCommandFactory { get; }
}