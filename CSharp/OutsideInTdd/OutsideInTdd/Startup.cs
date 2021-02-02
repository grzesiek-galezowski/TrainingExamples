using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OutsideInTdd
{
    public class AppLogicRoot
    {
        public readonly TodoCommandFactory _todoCommandFactory;

        public AppLogicRoot(TodoCommandFactory todoCommandFactory)
        {
            _todoCommandFactory = todoCommandFactory;
        }
    }

    public class ServiceLogicRoot
    {
        private readonly TodoNoteDao _todoNoteDao;
        private readonly NoteParser _noteParser;
        private readonly AppLogicRoot _appLogicRoot;

        public ServiceLogicRoot()
        {
            _todoNoteDao = new TodoNoteDao();
            var todoCommandFactory = new TodoCommandFactory(_todoNoteDao);
            _appLogicRoot = new AppLogicRoot(todoCommandFactory);
            _noteParser = new NoteParser();
        }

        public async Task HandleAddTodo(HttpContext context)
        {
            var dto = await _noteParser.ReadNoteFrom(context.Request);
            _appLogicRoot._todoCommandFactory.CreateAddNoteCommand(dto).Execute();
        }

        public Task HandleGetAllTodos(HttpContext context)
        {
            var todoResponse = new TodoResponse(context);
            return _appLogicRoot._todoCommandFactory.CreateRetrieveAllNotesCommand(todoResponse).Execute();
        }
    }

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
                        .GetRequiredService<ServiceLogicRoot>()
                        .HandleAddTodo(context);
                });

                endpoints.MapGet("Todo", context =>
                {
                    return context.RequestServices
                        .GetRequiredService<ServiceLogicRoot>()
                        .HandleGetAllTodos(context);
                });

            });
        }

        //TODO hide all public fields!!!
    }
}
