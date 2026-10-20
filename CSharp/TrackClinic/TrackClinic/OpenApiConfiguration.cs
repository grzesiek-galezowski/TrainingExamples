using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

public static class OpenApiConfiguration
{
  public static void ConfigureOpenApiDocument(OpenApiOptions options)
  {
    options.AddDocumentTransformer((document, _, _) =>
    {
      document.Info.Title = "TrackClinic API"; //bug duplication
      document.Info.Version = "v1";
      document.Info.Description = "API for managing song projects and artist collaborations.";
      document.Info.Contact = new OpenApiContact
      {
        Name = "TrackClinic Support",
        Email = "support@trackclinic.example.com"
      };
      return Task.CompletedTask;
    });
  }
}