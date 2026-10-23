using Flurl.Http;
using TrackClinicServiceSpecification.Automation.Keycloak;

namespace TrackClinicServiceSpecification.Automation;

public record TrackClinicRequestBuilder(IFlurlRequest Request)
{
  private string _username = "admin-user";

  public async Task<IFlurlRequest> Build()
  {
    //bug get all the possible tokens earlier and pass them in the request builder to avoid the async call here
    var token = await SharedKeycloakInstance.GetToken(_username, "password");
    return Request.AllowAnyHttpStatus().WithOAuthBearerToken(token);
  }

  public TrackClinicRequestBuilder WithUnprivilegedUser()
  {
    return this with { _username = "unprivileged-user" };
  }
}