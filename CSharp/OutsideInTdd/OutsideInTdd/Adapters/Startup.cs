using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OutsideInTdd.Adapters
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
            services.AddRouting();
            services.AddSingleton(ctx => new ServiceLogicRoot());
            services.AddSingleton(ctx => ctx.GetRequiredService<ServiceLogicRoot>().Endpoints);
            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapPost("Todo", async context =>
                {
                    await context.RequestServices
                        .GetRequiredService<EndpointsRoot>()
                        .AddTodoEndpoint.HandleAsync(context);
                });

                endpoints.MapGet("Todo", context =>
                {
                    return context.RequestServices
                        .GetRequiredService<EndpointsRoot>()
                        .AllTodosEndpoint.HandleAsync(context);
                });

            });
        }

        //TODO hide all public fields!!!
    }
}
