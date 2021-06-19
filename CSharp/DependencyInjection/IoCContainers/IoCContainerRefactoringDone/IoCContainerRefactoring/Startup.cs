using Flurl.Http;
using IoCContainerRefactoring.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace IoCContainerRefactoring
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddOptions();
      services.Configure<NotificationsConfiguration>(Configuration.GetSection(nameof(NotificationsConfiguration)));
      services.AddEntityFrameworkInMemoryDatabase();

      //Scoped/Transient
      services.AddDbContext<WeatherForecastDbContext>(
        (ctx, options) => 
          options.UseInMemoryDatabase("Weather")
            .UseInternalServiceProvider(ctx));
      services.AddTransient<WeatherForecastController>();
      services.AddTransient<IWeatherForecastDao, WeatherForecastDao>();
      
      //Singletons
      services.AddSingleton<ITechSupport, TechSupportViaLogger>();
      services.AddSingleton<IPersistentWeatherForecastDtoFactory, PersistentWeatherForecastDtoFactory>();
      services.AddSingleton<IWeatherForecastDtoFactory, WeatherForecastDtoFactory>();
      services.AddSingleton<IFlurlClient>(
          provider => new FlurlClient(
              provider.GetRequiredService<IOptions<NotificationsConfiguration>>().Value.BaseUrl
      ));
      services.AddSingleton<IEventPipe, EventPipe>();
      services.AddSingleton<IIdGenerator, IdGenerator>();
      services.AddSingleton<IWeatherCommandFactory, WeatherCommandFactory>();
      
      services.AddControllers().AddControllersAsServices();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
