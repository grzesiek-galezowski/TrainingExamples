using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace TrackClinic;

public static class OpenApiConfiguration
{
  public static void ConfigureOpenApiDocument(OpenApiOptions options)
  {
    options.AddDocumentTransformer((document, _, _) =>
    {
      document.Info.Title = OpenApiConstants.Title;
      document.Info.Version = OpenApiConstants.Version;
      document.Info.Description = OpenApiConstants.Description;
      document.Info.Contact = new OpenApiContact
      {
        Name = OpenApiConstants.ContactName,
        Email = OpenApiConstants.ContactEmail
      };
      return Task.CompletedTask;
    });
  }
}