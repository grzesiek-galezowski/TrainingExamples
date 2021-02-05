namespace OutsideInTdd.App
{
    public class AppLogicRoot
    {
        public TodoCommandFactory CommandFactory { get; }

        public AppLogicRoot(ITodoNoteDao todoNoteDao)
        {
            CommandFactory = new TodoCommandFactory(todoNoteDao);
        }
    }
}