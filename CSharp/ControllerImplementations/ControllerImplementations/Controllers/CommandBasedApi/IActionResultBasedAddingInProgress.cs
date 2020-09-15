using Microsoft.AspNetCore.Mvc;

namespace ControllerImplementations.Controllers.CommandBasedApi
{
    public interface IActionResultBasedAddingInProgress : IAddingInProgress
    {
        IActionResult Result();
    }
}