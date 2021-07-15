using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ModularMonolith.NotifyCustomer;

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
      //bug logger
      services.AddSingleton(context => new
        MonolithApplicationLogicCompositionRoot(new EmailCustomerNotifications()));
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
        var monolith = endpoints.ServiceProvider.GetRequiredService<MonolithApplicationLogicCompositionRoot>();

        endpoints.MapGet("/shop/products", async context =>
        {
          await monolith
            .GetProductsEndpoint.Handle(
              context.Request,
              context.Response,
              context.RequestAborted);
        });
        //bug the routes are messed up...
        endpoints.MapPost("/shop/products/", async context =>
        {
          await monolith
            .BuyProductEndpoint.Handle(
              context.Request,
              context.Response,
              context.RequestAborted);
        });

        //bug the routes are messed up...
        endpoints.MapPost("/orders/states/", async context =>
        {
          await monolith
            .UpdateOrderEndpoint.Handle(
              context.Request,
              context.Response,
              context.RequestAborted);
        });
      });
    }

  }
}
