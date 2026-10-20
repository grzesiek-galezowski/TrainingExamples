using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using Core.Maybe;
using Core.NullableReferenceTypesExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace TrackClinic;

public static class AuthorizationConfiguration
{
  public const string DefaultPolicyName = "ArtistPolicy";

  public static void ConfigureBearerTokenHandling(JwtBearerOptions options, AppConfig appConfig)
  {
    options.Authority = appConfig.Keycloak.Authority.OrThrow();
    options.MetadataAddress = appConfig.Keycloak.MetadataAddress.OrThrow();
    options.RequireHttpsMetadata = appConfig.Keycloak.RequireHttpsMetadata;
    options.TokenValidationParameters.ValidateAudience = false;
    options.Events = new JwtBearerEvents
    {
      OnTokenValidated = async context =>
      {
        var claimsIdentity = GetClaimsIdentityFrom(context);
        var realmAccess = FindKeycloakRealmAccessClaimIn(context);
        if (claimsIdentity.HasValue && realmAccess.HasValue)
        {
          AddRealmRolesAsRoleClaims(claimsIdentity.Value(), realmAccess.Value());
        }
      }
    };
  }

  private static Maybe<ClaimsIdentity> GetClaimsIdentityFrom(TokenValidatedContext context)
  {
    return context.Principal.ToMaybe().Select(p => p.Identity).Cast<IIdentity, ClaimsIdentity>();
  }

  private static void AddRealmRolesAsRoleClaims(ClaimsIdentity claimsIdentity, Claim realmAccess)
  {
    using var doc = JsonDocument.Parse(realmAccess.Value);
    if (doc.RootElement.TryGetProperty("roles", out var roles))
    {
      foreach (var role in roles.EnumerateArray())
      {
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.GetString()!));
      }
    }
  }

  private static Maybe<Claim> FindKeycloakRealmAccessClaimIn(TokenValidatedContext context)
  {
    return context.Principal.ToMaybe().SelectMaybe(p => p.FindFirst("realm_access").ToMaybe());
  }

  public static void ConfigureRoles(AuthorizationOptions options)
  {
    options.AddPolicy(
      AuthorizationConfiguration.DefaultPolicyName,
      policy => policy.RequireRole("artist", "admin")); //bug really? More tests needed
  }
}