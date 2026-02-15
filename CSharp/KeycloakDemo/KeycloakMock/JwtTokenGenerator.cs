using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace KeycloakMock;

public record SigningKeyInfo(string Modulus, string Exponent, string Kid, string Algorithm);

public class JwtTokenGenerator : IDisposable
{
    public string Authority { get; }
    public string Audience { get; }
    private readonly SigningCredentials _signingCredentials;
    private readonly RSA _rsa;
    private readonly string _kid;

    public JwtTokenGenerator(string authority, string audience)
    {
        Authority = authority;
        Audience = audience;
        _kid = Guid.NewGuid().ToString("N");
        _rsa = RSA.Create(2048);
        _signingCredentials = new SigningCredentials(
            new RsaSecurityKey(_rsa)
            {
                KeyId = _kid
            },
            SecurityAlgorithms.RsaSha256
        );
    }

    public SigningKeyInfo GetSigningKeyInfo()
    {
        var keyParameters = _rsa.ExportParameters(false);
        var modulus = Base64UrlEncode(keyParameters.Modulus!);
        var exponent = Base64UrlEncode(keyParameters.Exponent!);
        return new SigningKeyInfo(modulus, exponent, _kid, "RS256");
    }

    public string CreateToken(IReadOnlyList<Claim>? customClaims = null, DateTime? expiresAt = null)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Iss, $"{Authority}/"),
            new(JwtRegisteredClaimNames.Aud, Audience)
        };
        if (customClaims != null)
        {
            claims.AddRange(customClaims);
        }

        var now = DateTime.UtcNow;
        var expires = expiresAt ?? now.AddHours(1);

        // If expires is in the past, adjust NotBefore to be before expires to create a valid (but expired) token
        var notBefore = expires < now ? expires.AddMinutes(-1) : now;
        var issuedAt = expires < now ? expires.AddMinutes(-1) : now;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            NotBefore = notBefore,
            IssuedAt = issuedAt,
            Expires = expires,
            SigningCredentials = _signingCredentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static string Base64UrlEncode(byte[] arg)
    {
        var result = Convert.ToBase64String(arg);
        result = result.Split('=')[0];
        result = result.Replace('+', '-');
        result = result.Replace('/', '_');
        return result;
    }

    public void Dispose()
    {
        _rsa.Dispose();
        GC.SuppressFinalize(this);
    }
}
