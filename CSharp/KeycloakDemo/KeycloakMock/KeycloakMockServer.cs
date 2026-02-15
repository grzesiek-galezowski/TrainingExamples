using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace KeycloakMock;

public class KeycloakMockServer : IDisposable
{
    private readonly WireMockServer _server;
    private readonly JwtTokenGenerator _tokenGenerator;

    public string Authority => _tokenGenerator.Authority;
    public string Audience => _tokenGenerator.Audience;

    public KeycloakMockServer(int port, bool useSsl, string audience, string realm = "master")
    {
        var settings = new WireMockServerSettings
        {
            Port = port
        };

        if (useSsl)
        {
            settings.Urls = new[] { $"https://localhost:{port}" };
        }

        _server = WireMockServer.Start(settings);

        var authorityUrl = $"{_server.Urls[0]}/{realm}";
        _tokenGenerator = new JwtTokenGenerator(authorityUrl, audience);

        ConfigureOpenIdConfiguration();
        ConfigureJwksEndpoint();
    }

    public static KeycloakMockServer Start(int port, bool useSsl, string audience, string realm = "master")
    {
        return new KeycloakMockServer(port, useSsl, audience, realm);
    }

    public string CreateToken(IReadOnlyList<System.Security.Claims.Claim>? customClaims = null, DateTime? expiresAt = null)
    {
        return _tokenGenerator.CreateToken(customClaims, expiresAt);
    }

    private void ConfigureOpenIdConfiguration()
    {
        var realm = _tokenGenerator.Authority;
        
        _server
            .Given(Request.Create()
                .UsingGet()
                .WithPath("/*/.well-known/openid-configuration"))
            .AtPriority(1)
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json; charset=utf-8")
                .WithBodyAsJson(new
                {
                    issuer = $"{realm}/",
                    authorization_endpoint = $"{realm}/protocol/openid-connect/auth",
                    token_endpoint = $"{realm}/protocol/openid-connect/token",
                    userinfo_endpoint = $"{realm}/protocol/openid-connect/userinfo",
                    jwks_uri = $"{realm}/.well-known/jwks.json",
                    end_session_endpoint = $"{realm}/protocol/openid-connect/logout",
                    introspection_endpoint = $"{realm}/protocol/openid-connect/token/introspect",
                    grant_types_supported = new[] { "authorization_code", "implicit", "refresh_token", "password", "client_credentials" },
                    response_types_supported = new[] { "code", "none", "id_token", "token", "id_token token", "code id_token", "code token", "code id_token token" },
                    subject_types_supported = new[] { "public", "pairwise" },
                    id_token_signing_alg_values_supported = new[] { "PS384", "ES384", "RS384", "HS256", "HS512", "ES256", "RS256", "HS384", "ES512", "PS256", "PS512", "RS512" },
                    id_token_encryption_alg_values_supported = new[] { "RSA-OAEP", "RSA-OAEP-256", "RSA1_5" },
                    id_token_encryption_enc_values_supported = new[] { "A256GCM", "A192GCM", "A128GCM", "A128CBC-HS256", "A192CBC-HS384", "A256CBC-HS512" },
                    userinfo_signing_alg_values_supported = new[] { "PS384", "ES384", "RS384", "HS256", "HS512", "ES256", "RS256", "HS384", "ES512", "PS256", "PS512", "RS512", "none" },
                    request_object_signing_alg_values_supported = new[] { "PS384", "ES384", "RS384", "HS256", "HS512", "ES256", "RS256", "HS384", "ES512", "PS256", "PS512", "RS512", "none" },
                    response_modes_supported = new[] { "query", "fragment", "form_post", "query.jwt", "fragment.jwt", "form_post.jwt", "jwt" },
                    registration_endpoint = $"{realm}/clients-registrations/openid-connect",
                    token_endpoint_auth_methods_supported = new[] { "private_key_jwt", "client_secret_basic", "client_secret_post", "tls_client_auth", "client_secret_jwt" },
                    token_endpoint_auth_signing_alg_values_supported = new[] { "PS384", "ES384", "RS384", "HS256", "HS512", "ES256", "RS256", "HS384", "ES512", "PS256", "PS512", "RS512" },
                    claims_supported = new[] { "aud", "sub", "iss", "auth_time", "name", "given_name", "family_name", "preferred_username", "email", "acr" },
                    claim_types_supported = new[] { "normal" },
                    claims_parameter_supported = true,
                    scopes_supported = new[] { "openid", "profile", "email", "address", "phone", "offline_access", "microprofile-jwt" },
                    request_parameter_supported = true,
                    request_uri_parameter_supported = true,
                    require_request_uri_registration = true,
                    code_challenge_methods_supported = new[] { "plain", "S256" },
                    tls_client_certificate_bound_access_tokens = true,
                    revocation_endpoint = $"{realm}/protocol/openid-connect/revoke",
                    revocation_endpoint_auth_methods_supported = new[] { "private_key_jwt", "client_secret_basic", "client_secret_post", "tls_client_auth", "client_secret_jwt" },
                    revocation_endpoint_auth_signing_alg_values_supported = new[] { "PS384", "ES384", "RS384", "HS256", "HS512", "ES256", "RS256", "HS384", "ES512", "PS256", "PS512", "RS512" },
                    backchannel_logout_supported = true,
                    backchannel_logout_session_supported = true
                })
            );
    }

    private void ConfigureJwksEndpoint()
    {
        var signingKeyInfo = _tokenGenerator.GetSigningKeyInfo();
        
        _server
            .Given(Request.Create()
                .UsingGet()
                .WithPath("/*/.well-known/jwks.json"))
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json; charset=utf-8")
                .WithBodyAsJson(new
                {
                    keys = new[]
                    {
                        new
                        {
                            kty = "RSA",
                            use = "sig",
                            n = signingKeyInfo.Modulus,
                            e = signingKeyInfo.Exponent,
                            kid = signingKeyInfo.Kid,
                            alg = signingKeyInfo.Algorithm
                        }
                    }
                })
            );
    }

    public void Dispose()
    {
        _tokenGenerator?.Dispose();
        _server?.Stop();
        _server?.Dispose();
        GC.SuppressFinalize(this);
    }
}
