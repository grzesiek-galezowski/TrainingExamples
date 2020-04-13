using System;
using Core.Maybe;
using Microsoft.AspNetCore.Mvc;

namespace ControllerImplementations.Controllers.CommandBasedApi
{
    internal class AddingInProgress : IActionResultBasedAddingInProgress
    {
        private Maybe<IActionResult> _actionResult = Maybe<IActionResult>.Nothing;

        public void SavedSuccessfully(PostDto postDto, string id)
        {
            _actionResult = new OkObjectResult(postDto).JustObject<IActionResult>();
        }

        public void FailedBecauseOf(Exception exception)
        {
            _actionResult = new JsonResult(exception)
            {
                StatusCode = 500
            }.JustObject<IActionResult>();
        }

        public IActionResult Result()
        {
            return _actionResult.Value();
        }
    }
}