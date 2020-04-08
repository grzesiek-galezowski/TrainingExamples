using System;

namespace ControllerImplementations.Controllers.CommandBasedApi
{
    public interface ILinkingInProgress
    {
        void FailedFor(string id1, string id2, Exception exception);
    }
}