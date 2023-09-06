
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.IdentityModel.Logging;

namespace SecureApp
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      builder.Services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options =>
      {
        builder.Configuration.Bind("Keycloak", options);
        options.Events = new JwtBearerEvents
        {
          OnMessageReceived = context =>
          {
            var accessToken = context.Request.Query["access_token"];

            if (!string.IsNullOrEmpty(accessToken))
            {
              context.Token = accessToken;
            }

            return Task.CompletedTask;
          },
          OnAuthenticationFailed = context =>
          {
            Console.WriteLine("Failed " + context.Exception);
            return Task.CompletedTask;
          },
          OnChallenge = context =>
          {
            Console.WriteLine("Challenge " + context.ErrorDescription + " " + context.AuthenticateFailure);
            return Task.CompletedTask;
          },
          OnForbidden = context =>
          {
            Console.WriteLine("Forbidden " + context.Result);
            return Task.CompletedTask;
          },
          OnTokenValidated = context =>
          {
            Console.WriteLine("Validated " + context.SecurityToken);
            return Task.CompletedTask;
          }
        };
      });
      builder.Services.AddAuthorization(options =>
      {
        options.AddPolicy("MyPolicy", policy =>
        {
          policy.Requirements.Add(new RoleRestrictedRequirement());
        });
      });
      builder.Services.AddSignalR();
      builder.Services.AddWebSockets(options => { });

      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
        IdentityModelEventSource.ShowPII = true;
      }

      app.UseAuthentication();
      app.UseAuthorization();

      app.MapHub<MyEchoHub>("echo").RequireAuthorization("MyPolicy");

      app.Run();
    }
  }

  public class MyEchoHub : Hub<IEcho>
  {
  }

  public interface IEcho
  {
  }
}