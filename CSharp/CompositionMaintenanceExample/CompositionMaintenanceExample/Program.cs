
namespace CompositionMaintenanceExample
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var b = WebApplication.CreateBuilder(args);

      // Add services to the container.

      b.Services.AddSingleton<ServiceLogicRoot>();
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController1());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController2());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController3());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController4());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController5());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController6());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController7());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController8());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController9());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController10());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController11());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController12());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController13());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController14());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController15());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController16());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController17());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController18());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController19());
      b.Services.AddTransient(c => c.GetRequiredService<ServiceLogicRoot>().CreateWeatherForecastController20());

      b.Services.AddControllers().AddControllersAsServices();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      b.Services.AddEndpointsApiExplorer();
      b.Services.AddSwaggerGen();

      var app = b.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}
