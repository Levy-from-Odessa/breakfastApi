namespace BuberBreakfast.ErrorController;

using Microsoft.AspNetCore.Mvc;
using BuberBreakfast.Controllers.ApiController;
public class ErrorController : ApiController
{
  [Route("/error")]
  public IActionResult Error()
  {
    return Problem();
  }

}