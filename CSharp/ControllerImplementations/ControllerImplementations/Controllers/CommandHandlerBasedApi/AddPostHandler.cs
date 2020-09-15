using System;
using System.Threading.Tasks;
using static Core.Maybe.Either<ControllerImplementations.Controllers.PostCreatedDto,ControllerImplementations.Controllers.CommandHandlerBasedApi.ErrorInfo>;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi
{
    public class AddPostHandler : IHandler<AddPostCommand>
    {
        private readonly IPostAssertions _postAssertions;
        private readonly IExistingPosts _existingPosts;
        private readonly IFollowers _followers;

        public AddPostHandler(
            IPostAssertions postAssertions, 
            IExistingPosts existingPosts, 
            IFollowers followers)
        {
            _postAssertions = postAssertions;
            _existingPosts = existingPosts;
            _followers = followers;
        }

        public async Task HandleAsync(AddPostCommand command)
        {
            try
            {
                _postAssertions.AssertContentIsOfRequiredLength(command);
                _postAssertions.AssertContentContainsNoInappropriateWords(command);
                command.Result = Result(await _existingPosts.AddAsync(command));
                await _followers.NotifyAsync(command);
            }
            catch (Exception e)
            {
                command.Result = Error(new ErrorInfo(e));
            }
        }
    }
}