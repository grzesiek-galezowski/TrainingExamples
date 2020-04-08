using Microsoft.AspNetCore.Mvc;

namespace ControllerImplementations.Controllers.CommandBasedApi
{
    public interface IActionResultBasedLinkingInProgress : ILinkingInProgress
    {
        IActionResult Result();
    }
}