using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ControllerImplementations.Controllers.CommandBasedApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IResultInProgressFactory _resultInProgressFactory;
        private readonly ICommandFactory _commandFactory;

        public PostController(
            IResultInProgressFactory resultInProgressFactory, 
            ICommandFactory commandFactory)
        {
            _resultInProgressFactory = resultInProgressFactory;
            _commandFactory = commandFactory;
        }

        [HttpPost]
        [Route("posts")]
        public async Task<IActionResult> AddPost([FromBody] PostDto post)
        {
            var addingInProgress = _resultInProgressFactory.AddingInProgress();
            var addPostCommand = _commandFactory.CreateAddPostCommand(post, addingInProgress);
            await addPostCommand.ExecuteAsync();
            return addingInProgress.Result();
        }

        [HttpPost]
        [Route("posts/{id1}/link/{id2}")]
        public async Task<IActionResult> LinkPostsPost(string id1, string id2)
        {
            var addingInProgress = _resultInProgressFactory.LinkingInProgress();
            var linkPostsCommand = _commandFactory.CreateLinkPostsCommand(id1, id2, addingInProgress);
            await linkPostsCommand.ExecuteAsync();
            return addingInProgress.Result();
        }

    }
}
