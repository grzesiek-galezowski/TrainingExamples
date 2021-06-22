using IoCContainerRefactoring.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

      //Scoped & Transient
      services.AddDbContext<WeatherForecastDbContext>(
        (ctx, options) => 
          options.UseInMemoryDatabase("Weather")
            .UseInternalServiceProvider(ctx));
      services.AddTransient(
        provider => provider.GetService<ServiceLogicRoot>()
          .CreateWeatherForecastController(
            provider.GetService<WeatherForecastDbContext>()));
      
      //Singletons
      services.AddSingleton(provider => new ServiceLogicRoot(provider.GetRequiredService<IOptions<NotificationsConfiguration>>().Value.BaseUrl, provider.GetService<ILoggerFactory>()));
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
