using Microsoft.AspNetCore.Mvc;
using PloehKata.Ports;

namespace PloehKata.Adapters
{
  public interface IActionResultBasedConnectionInProgress : IConnectionInProgress
  {
    IActionResult ToActionResult();
  }
}