using Microsoft.AspNetCore.Mvc;

using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models.Breakfast;
// using BuberBreakfast.Controllers;
using BuberBreakfast.Services.IBreakfastService;
using BuberBreakfast.Services.BreakfastService;


[ApiController]
[Route("[controller]")]
public class BreakfastsController : ControllerBase
{
  private readonly IBreakfastService _breakfastService;
  public BreakfastsController(IBreakfastService breakfastService)
  {
    _breakfastService = breakfastService;
  }
  [HttpPost]
  public IActionResult CreateBreakfast(CreateBreakfastRequest req)
  {
    var breakfast = new Breakfast(
      Guid.NewGuid(),
      req.Name,
      req.Description,
      req.StartDateTime,
      req.EndDateTime,
      DateTime.UtcNow,
      req.Savory,
      req.Sweet
    );

    var response = new BreakfastResponse(
      breakfast.Id,
      breakfast.Name,
      breakfast.Description,
      breakfast.StartDateTime,
      breakfast.EndDateTime,
      breakfast.LastModifiedDateTime,
      breakfast.Savory,
      breakfast.Sweet
    );

    _breakfastService.CreateBreakfast(breakfast);

    return CreatedAtAction(
      actionName: nameof(GetBreakfast),
      routeValues: new { id = breakfast.Id },
      value: response
    );
  }
  [HttpGet("{id:guid}")]
  public IActionResult GetBreakfast(Guid id)
  {
    Breakfast breakfast = _breakfastService.GetBreakfast(id);

    var response = new BreakfastResponse(
      breakfast.Id,
      breakfast.Name,
      breakfast.Description,
      breakfast.StartDateTime,
      breakfast.EndDateTime,
      breakfast.LastModifiedDateTime,
      breakfast.Savory,
      breakfast.Sweet
    );
    return Ok(response);
  }
  [HttpPut("{id:guid}")]
  public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest req)
  {
    var response = new Breakfast(
      id,
      req.Name,
      req.Description,
      req.StartDateTime,
      req.EndDateTime,
      DateTime.UtcNow,
      req.Savory,
      req.Sweet
    );
    _breakfastService.UpsertBreakfast(response);
    return NoContent();
  }
  [HttpDelete("{id:guid}")]
  public IActionResult deleteBreakfast(Guid id)
  {
    _breakfastService.DeleteBreakfast(id);
    return NoContent();
  }
}
