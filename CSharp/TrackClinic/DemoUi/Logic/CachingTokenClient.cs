using System.IdentityModel.Tokens.Jwt;
using Core.NullableReferenceTypesExtensions;
using DemoUi.Adapters.Configuration;
using DemoUi.Adapters.Keycloak;
using Microsoft.Extensions.Caching.Memory;
using ServiceBuildingBlocks;

namespace DemoUi.Logic;

public class CachingTokenClient(
  KeycloakTokenClient tokenClient,
  KeycloakConfig keycloakConfig)
{
  private const string TokenKey = "bearer_token";
  private readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

  public async Task<string> GetBearerTokenAsync(CancellationToken cancellationToken)
  {
    var entry = await _cache.GetOrCreateAsync(
      TokenKey,
      async entry =>
      {
        var token = await tokenClient.GetPasswordToken(
          keycloakConfig.MetadataAddress,
          KeycloakConstants.ClientId,
          KeycloakConstants.ArtistUser,
          KeycloakConstants.Password,
          cancellationToken);

        var validTo = new JwtSecurityTokenHandler()
          .ReadJwtToken(token).ValidTo;
        entry.AbsoluteExpiration = validTo - TimeSpan.FromSeconds(30);
        return token;
      });

    return entry.OrThrow();
  }
}