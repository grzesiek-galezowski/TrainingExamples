
namespace TrainReservationService;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddAuthorization();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseAuthorization();

    app.MapPost("/reserveSeats", (HttpContext httpContext) =>
      {

      })
      .WithName("ReserveSeats")
      .WithOpenApi();

    app.Run();
  }
}
