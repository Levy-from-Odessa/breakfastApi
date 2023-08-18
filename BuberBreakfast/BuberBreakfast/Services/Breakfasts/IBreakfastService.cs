using BuberBreakfast.Models.Breakfast;

namespace BuberBreakfast.Services.IBreakfastService;
public interface IBreakfastService
{
  void CreateBreakfast(Breakfast breakfast);
  Breakfast GetBreakfast(Guid id);
  void UpsertBreakfast(Breakfast breakfast);
  void DeleteBreakfast(Guid id);
}