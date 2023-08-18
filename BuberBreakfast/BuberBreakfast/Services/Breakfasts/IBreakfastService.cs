using BuberBreakfast.Models.Breakfast;
using ErrorOr;

// namespace BuberBreakfast.Services.IBreakfastService;
public interface IBreakfastService
{
  ErrorOr<Created> CreateBreakfast(Breakfast breakfast);
  ErrorOr<Breakfast> GetBreakfast(Guid id);
  ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast);
  ErrorOr<Deleted> DeleteBreakfast(Guid id);
}