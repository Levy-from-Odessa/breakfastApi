using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.ErrorController;
public class ErrorController : ControllerBase
{
  [Route("/error")]
  public IActionResult Error()
  {
    return Problem();
  }

}