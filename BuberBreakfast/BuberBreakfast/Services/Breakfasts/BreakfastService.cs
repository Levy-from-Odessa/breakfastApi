
using System.Collections.Generic;
using ErrorOr;
using BuberBreakfast.ServiceErrors;
// using BuberBreakfast.Services.IBreakfastService;
using BuberBreakfast.Models.Breakfast;

namespace BuberBreakfast.Services.BreakfastService;

public class BreakfastService : IBreakfastService
{
  private static readonly Dictionary<Guid, Breakfast> _breakfasts = new();
  public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
  {
    _breakfasts.Add(breakfast.Id, breakfast);
    return Result.Created;
  }
  public ErrorOr<Breakfast> GetBreakfast(Guid id)
  {
    if (_breakfasts.TryGetValue(id, out var breakfast))
    {
      return breakfast;
    }
    return Errors.Breakfast.NotFound;
  }
  public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast)
  {
    var IsCreated = !_breakfasts.ContainsKey(breakfast.id);

    _breakfasts[breakfast.Id] = breakfast;
    return new UpsertedBreakfast(IsCreated);
  }
  public ErrorOr<Deleted> DeleteBreakfast(Guid id)
  {
    _breakfasts.Remove(id);
    return Result.Deleted;
  }
}