namespace ControllerImplementations.Controllers.CommandHandlerBasedApi
{
    public interface IPostAssertions
    {
        void AssertContentIsOfRequiredLength(AddPostCommand command);
        void AssertContentContainsNoInappropriateWords(AddPostCommand command);
    }
}