using BuberBreakfast.Services.IBreakfastService;
using BuberBreakfast.Services.BreakfastService;

var builder = WebApplication.CreateBuilder(args);

{
  builder.Services.AddControllers();
  builder.Services.AddScoped<IBreakfastService, BreakfastService>();
}

var app = builder.Build();

{
  app.UseExceptionHandler("/error");
  app.UseHttpsRedirection();
  app.MapControllers();
  app.Run();
}
