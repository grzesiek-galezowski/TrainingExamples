using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TddXt.SimpleNlp;

namespace FrodoEntersTheRoom
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
      services.AddSingleton(ctx => new ServiceLogicRoot());
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
        endpoints.MapPost("messages", async context =>
        {
          var body = await BodyFrom(context);
          var recognitionModel = TrainRecognitionModel();
          var recognitionResult = recognitionModel.Recognize(body);
          
          if (recognitionResult.TopIntent == "KillCharacter")
          {
            var intent = CreateKillCharacterIntentFrom(recognitionResult);

            intent.Apply();
          }
        });
      });
    }

    private static RecognitionModel TrainRecognitionModel()
    {
      var recognitionModel = new RecognitionModel();
      recognitionModel.AddEntity("Character", "Gandalf", new[] {"gandalf"});
      recognitionModel.AddEntity("Kill", "Kill", new[] {"kill", "destroy", "attack"});
      recognitionModel.AddIntent("KillCharacter", new[] {"Kill", "Character"});
      return recognitionModel;
    }

    private static async Task<string> BodyFrom(HttpContext context)
    {
      using (var streamReader = new StreamReader(context.Request.Body))
      {
        return await streamReader.ReadToEndAsync();
      }
    }

    private static KillCharacterIntent CreateKillCharacterIntentFrom(RecognitionResult recognitionResult)
    {
      var characterName =
        recognitionResult.Entities.Single(
          entity => entity.Entity.Equals(
            EntityName.Value("Character"))).CanonicalForm.ToString();

      return new KillCharacterIntent(
        characterName,
        new Dialog(), //initial dialog state and cast
        new ResponsePhraseTodo());
    }
  }
}
