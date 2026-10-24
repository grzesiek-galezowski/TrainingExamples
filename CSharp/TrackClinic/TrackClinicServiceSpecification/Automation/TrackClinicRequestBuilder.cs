using Flurl.Http;

namespace TrackClinicServiceSpecification.Automation;

public record TrackClinicRequestBuilder
{
  private string? _token;
  private readonly TestAuthTokens _testAuthTokens;
  private readonly IFlurlRequest _request;
  private TimeSpan _delay = TimeSpan.Zero;

  public TrackClinicRequestBuilder(IFlurlRequest request, TestAuthTokens testAuthTokens)
  {
    _token = testAuthTokens.AdminToken;
    _request = request;
    _testAuthTokens = testAuthTokens;
  }


  public IFlurlRequest Build()
  {
    var request = _request.AllowAnyHttpStatus();
    if (_token != null)
    {
      request = request.WithOAuthBearerToken(_token);
    }

    return request;
  }

  public async Task WaitIfNeeded(CancellationToken cancellationToken)
  {
    if (_delay != TimeSpan.Zero)
    {
      await Task.Delay(_delay, cancellationToken);
    }
  }

  public TrackClinicRequestBuilder WithDelayBeforeSending(TimeSpan delay)
  {
    return this with { _delay = delay };
  }

  public TrackClinicRequestBuilder WithUnprivilegedUser()
  {
    return this with { _token = _testAuthTokens.UnprivilegedToken };
  }

  public TrackClinicRequestBuilder WithArtistUser()
  {
    return this with { _token = _testAuthTokens.ArtistToken };
  }

  public TrackClinicRequestBuilder WithoutToken()
  {
    return this with { _token = null };
  }

  public TrackClinicRequestBuilder WithInvalidToken()
  {
    return this with { _token = "invalid-token" };
  }
}