using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ModularMonolith
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
      services.AddEntityFrameworkInMemoryDatabase();

      //Scoped & Transient
      services.AddDbContext<ShopDbContext>(
        (ctx, options) =>
          options.UseInMemoryDatabase("Weather")
            .UseInternalServiceProvider(ctx));
      //bug logger
      services.AddSingleton(context => new Monolith(new DaoFactory(context)));
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "ModularMonolith", Version = "v1" });

      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ModularMonolith v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        var monolith = endpoints.ServiceProvider.GetRequiredService<Monolith>();

        endpoints.MapGet("/shop/products", async context =>
        {
          await monolith
            .GetProductsEndpoint.HandleAsync(
              context.Request,
              context.Response,
              context.RequestAborted);
        });
        endpoints.MapPost("/shop/products", async context =>
        {
          await monolith
            .BuyProductEndpoint.HandleAsync(
              context.Request,
              context.Response,
              context.RequestAborted);
        });
      });
    }

  }
}
