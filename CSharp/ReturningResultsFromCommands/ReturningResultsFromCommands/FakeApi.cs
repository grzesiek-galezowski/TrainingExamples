namespace ReturningResultsFromCommands;

public class FakeApi : ISomeKindOfApi
{
    public int Send(Todo todo)
    {
        throw new NotImplementedException();
    }
}