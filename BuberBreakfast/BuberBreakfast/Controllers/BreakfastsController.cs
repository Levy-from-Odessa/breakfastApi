
using BuberBreakfast.Controllers.ApiController;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using BuberBreakfast.ServiceErrors;

using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models.Breakfast;
// using BuberBreakfast.Controllers;
// using BuberBreakfast.Services.IBreakfastService;
using BuberBreakfast.Services.BreakfastService;


public class BreakfastsController : ApiController
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

    ErrorOr<Created> createBreakfastRes = _breakfastService.CreateBreakfast(breakfast);

    return createBreakfastRes.Match<IActionResult>(
      created => CreatedAsGetBreakfast(breakfast),
      errors => Problem(errors)
    );

  }
  [HttpGet("{id:guid}")]
  public IActionResult GetBreakfast(Guid id)
  {
    ErrorOr<Breakfast> getBreakfastRes = _breakfastService.GetBreakfast(id);

    return getBreakfastRes.Match<IActionResult>(
      breakfast => Ok(MapBreakfastToResponse(breakfast)),
      errors => Problem(errors)
    );

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

    ErrorOr<UpsertedBreakfast> upsertedResult = _breakfastService.UpsertBreakfast(response);

    return upsertedResult.Match(
      updated => updated.IsCreated ? CreatedAsGetBreakfast(response) : NoContent(),
      errors => Problem(errors)
    );

  }
  [HttpDelete("{id:guid}")]
  public IActionResult deleteBreakfast(Guid id)
  {
    ErrorOr<Deleted> deleteBreakfastRes = _breakfastService.DeleteBreakfast(id);
    return deleteBreakfastRes.Match<IActionResult>(
      deleted => NoContent(),
      errors => Problem(errors)
    );
  }


  private static BreakfastResponse MapBreakfastToResponse(Breakfast breakfast)
  {
    return new BreakfastResponse(
      breakfast.Id,
      breakfast.Name,
      breakfast.Description,
      breakfast.StartDateTime,
      breakfast.EndDateTime,
      breakfast.LastModifiedDateTime,
      breakfast.Savory,
      breakfast.Sweet
    );
  }

  private CreatedAtActionResult CreatedAsGetBreakfast(Breakfast breakfast)
  {
    return CreatedAtAction(
      actionName: nameof(GetBreakfast),
      routeValues: new { id = breakfast.Id },
      value: MapBreakfastToResponse(breakfast)
    );
  }
}
