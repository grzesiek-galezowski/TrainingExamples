using System;
using Microsoft.AspNetCore.Mvc;

namespace ControllerImplementations.Controllers.CommandBasedApi
{
    internal class AddingInProgress : IActionResultBasedAddingInProgress
    {
        public void SavedSuccessfully(PostDto postDto, string id)
        {
            throw new NotImplementedException();
        }

        public void FailedBecauseOf(Exception exception)
        {
            throw new NotImplementedException();
        }

        public IActionResult Result()
        {
            throw new NotImplementedException();
        }
    }
}