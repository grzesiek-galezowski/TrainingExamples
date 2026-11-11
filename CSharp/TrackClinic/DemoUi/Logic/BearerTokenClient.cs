using System.IdentityModel.Tokens.Jwt;
using DemoUi.Adapters.Configuration;
using DemoUi.Adapters.Keycloak;
using ServiceBuildingBlocks;

namespace DemoUi.Logic;

public class BearerTokenClient(KeycloakTokenClient tokenClient, KeycloakConfig keycloakConfig)
{
  //bug what does this class do? why not just use KeycloakTokenClient directly?
  private string? _cachedBearerToken;
  private DateTime _tokenExpiration = DateTime.MinValue;
  private readonly SemaphoreSlim _tokenLock = new(1, 1);

  public async Task<string?> TryGetBearerTokenAsync(CancellationToken cancellationToken)
  {
    if (DateTime.UtcNow < _tokenExpiration && !string.IsNullOrEmpty(_cachedBearerToken))
    {
      return _cachedBearerToken;
    }

    await _tokenLock.WaitAsync(cancellationToken);
    try
    {
      if (DateTime.UtcNow < _tokenExpiration && !string.IsNullOrEmpty(_cachedBearerToken))
      {
        return _cachedBearerToken;
      }

      var token = await tokenClient.GetPasswordToken(
        keycloakConfig.MetadataAddress,
        KeycloakConstants.ClientId,
        KeycloakConstants.ArtistUser,
        KeycloakConstants.Password,
        cancellationToken);

      _cachedBearerToken = token;
      _tokenExpiration = new JwtSecurityTokenHandler()
        .ReadJwtToken(token)
        .ValidTo - TimeSpan.FromSeconds(30);
      return _cachedBearerToken;
    }
    finally
    {
      _tokenLock.Release();
    }
  }
}