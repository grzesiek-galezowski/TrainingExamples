namespace OutsideInTdd.App
{
    public class AppLogicRoot
    {
        public ITodoCommandFactory CommandFactory { get; }

        public AppLogicRoot(ITodoNoteDao todoNoteDao)
        {
            CommandFactory = new TodoCommandFactory(todoNoteDao);
        }
    }
}