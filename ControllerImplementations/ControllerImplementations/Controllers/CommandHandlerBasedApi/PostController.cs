using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            try
            {
                var addPostCommand = new AddPostCommand
                {
                    Post = post
                };
                await _addPostHandler.HandleAsync(addPostCommand);

                return Ok(addPostCommand.ResponseCreatedPost);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("posts/{id1}/link/{id2}")]
        public async Task<IActionResult> LinkPostsPost(string id1, string id2)
        {
            try
            {
                var addPostCommand = new LinkPostsCommand
                {
                    Id1 = id1,
                    Id2 = id2,
                };
                await _linkPostsHandler.HandleAsync(addPostCommand);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
    }
}
}
