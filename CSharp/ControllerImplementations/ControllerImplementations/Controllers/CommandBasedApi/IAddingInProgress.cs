using System;

namespace ControllerImplementations.Controllers.CommandBasedApi
{
    public interface IAddingInProgress
    {
        void SavedSuccessfully(PostDto postDto, string id);
        void FailedBecauseOf(Exception exception);
    }
}