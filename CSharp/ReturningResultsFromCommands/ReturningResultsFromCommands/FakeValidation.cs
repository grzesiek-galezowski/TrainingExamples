namespace ReturningResultsFromCommands;

public class FakeValidation : IValidation
{
    public bool IsSuccessfulFor(Todo todo)
    {
        throw new NotImplementedException();
    }
}