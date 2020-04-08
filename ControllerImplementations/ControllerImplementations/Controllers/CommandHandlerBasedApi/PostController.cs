using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Maybe;
using Microsoft.AspNetCore.Mvc;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IHandler<AddPostCommand> _addPostHandler;
        private readonly IHandler<LinkPostsCommand> _linkPostsHandler;

        public PostController(
            IHandler<AddPostCommand> addPostHandler,
            IHandler<LinkPostsCommand> linkPostsHandler)
        {
            _addPostHandler = addPostHandler;
            _linkPostsHandler = linkPostsHandler;
        }

        [HttpPost]
        [Route("posts")]
        public async Task<IActionResult> AddPost([FromBody] PostDto post)
        {
            var addPostCommand = new AddPostCommand
            {
                Post = post //bug commands should not really contain DTOs
            };
            await _addPostHandler.HandleAsync(addPostCommand);

            return addPostCommand.Error
                .Select(info => (IActionResult)BadRequest(info.E))
                .OrElse(Ok(addPostCommand.ResponseCreatedPost));
        }

        [HttpPost]
        [Route("posts/{id1}/link/{id2}")]
        public async Task<IActionResult> LinkPostsPost(string id1, string id2)
        {
            var linkPostsCommand = new LinkPostsCommand
            {
                Id1 = id1,
                Id2 = id2
            };
            await _linkPostsHandler.HandleAsync(linkPostsCommand);

            return linkPostsCommand.Error
                .Select(info => (IActionResult)BadRequest(info.E))
                .OrElse(Ok(linkPostsCommand.Id1));

        }
    }
}

