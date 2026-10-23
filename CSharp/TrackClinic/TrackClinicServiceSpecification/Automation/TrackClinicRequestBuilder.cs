using Flurl.Http;

namespace TrackClinicServiceSpecification.Automation;

public record TrackClinicRequestBuilder
{
  private string _token;
  private readonly TestAuthTokens _testAuthTokens;
  private readonly IFlurlRequest _request;

  public TrackClinicRequestBuilder(IFlurlRequest request, TestAuthTokens testAuthTokens)
  {
    _token = testAuthTokens.AdminToken;
    _request = request;
    _testAuthTokens = testAuthTokens;
  }


  public IFlurlRequest Build()
  {
    return _request.AllowAnyHttpStatus().WithOAuthBearerToken(_token);
  }

  public TrackClinicRequestBuilder WithUnprivilegedUser()
  {
    return this with { _token = _testAuthTokens.UnprivilegedToken };
  }
}