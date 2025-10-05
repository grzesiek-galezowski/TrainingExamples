using System.Buffers.Text;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using WireMock;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;
using WireMock.Types;
using WireMock.Util;

namespace WiremockHttpsTest;

public class Tests
{
  [Test]
  public async Task Test1()
  {
    // Arrange
    var deviceId = Guid.NewGuid().ToString();
    var rsaKey1 = RSA.Create(4096);
    using var server = WireMockServer.Start(
      new WireMockServerSettings
      {
        ClientCertificateMode = ClientCertificateMode.RequireCertificate,
        AcceptAnyClientCertificate = true,
        UseSSL = true,
      });

    // Create a self-signed certificate for testing
    var caCert = CreateSelfSignedCaCertificate("WireMockTestCert");
    var provisionCert = CreateSelfSignedCaCertificate("ClientCert");

    SetupCaCertEndpoint(server, caCert);
    SetupSimpleEnrollEndpoint(server, caCert);

    // Act
    var response = await GetCaCerts(Pkcs12CollectionWith(provisionCert), server);
    AssertCaResponse(response);

    var csrPem = CreateCertificateCsrPem(deviceId, rsaKey1);
    var enrollResponse = await RequestSimpleEnroll(Pkcs12CollectionWith(provisionCert), server, csrPem);
    AssertSimpleenrollResponse(enrollResponse);
    var deviceCertificate = await ReadDeviceCertificate(enrollResponse);
    AssertEnrollCertificate(deviceCertificate, deviceId);
  }

  private static void AssertEnrollCertificate(X509Certificate2 deviceCertificate, string deviceId)
  {
    Assert.That(deviceCertificate, Is.Not.Null);
    Assert.That(deviceCertificate.HasPrivateKey, Is.False);
    Assert.That(deviceCertificate.Subject, Is.EqualTo("CN=" + deviceId));
  }

  private static X509Certificate2Collection Pkcs12CollectionWith(X509Certificate2 provisionCert)
  {
    return X509CertificateLoader.LoadPkcs12Collection(
      provisionCert.Export(X509ContentType.Pfx),
      null,
      X509KeyStorageFlags.Exportable);
  }

  private static async Task<X509Certificate2> ReadDeviceCertificate(HttpResponseMessage enrollResponse)
  {
    var base64Cert = await enrollResponse.Content.ReadAsStringAsync();
    var certBase64 = Convert.FromBase64String(base64Cert);
    var deviceCertificate = X509CertificateLoader.LoadPkcs12(
      certBase64,
      null,
      X509KeyStorageFlags.Exportable);
    return deviceCertificate;
  }

  private static void AssertSimpleenrollResponse(HttpResponseMessage enrollResponse)
  {
    Assert.That(enrollResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    Assert.That(enrollResponse.Content.Headers.GetValues("content-type").Single(), Is.EqualTo("application/pkcs12"));
    Assert.That(enrollResponse.Content.ReadAsStringAsync().Result, Is.Not.EqualTo(""));
  }

  private static async Task<HttpResponseMessage> RequestSimpleEnroll(X509Certificate2Collection certificates, WireMockServer server,
    string csrPem)
  {
    using var httpMessageHandler2 = HttpMessageHandler(certificates);
    var client = new HttpClient(httpMessageHandler2);
    var enrollResponse = await client
      .PostAsync("https://localhost:" + server.Ports[0] + "/simpleenroll", new StringContent(csrPem));
    return enrollResponse;
  }

  private static async Task<HttpResponseMessage> GetCaCerts(X509Certificate2Collection certificates, WireMockServer server)
  {
    using var httpMessageHandler1 = HttpMessageHandler(certificates);
    using var httpClient = new HttpClient(httpMessageHandler1);
    var response = await httpClient
      .GetAsync("https://localhost:" + server.Ports[0] + "/cacert");
    return response;
  }

  private static void AssertCaResponse(HttpResponseMessage response)
  {
    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    Assert.That(response.Content.Headers.GetValues("content-type").Single(), Is.EqualTo("application/pkcs12"));
    Assert.That(response.Content.ReadAsStringAsync().Result, Is.Not.EqualTo(""));
  }

  private static string CreateCertificateCsrPem(string deviceId, RSA rsaKey1)
  {
    var csr = new CertificateRequest(
      new X500DistinguishedName("CN=" + deviceId),
      rsaKey1,
      HashAlgorithmName.SHA256,
      RSASignaturePadding.Pkcs1);
    var csrPem = csr.CreateSigningRequestPem(X509SignatureGenerator.CreateForRSA(rsaKey1, RSASignaturePadding.Pkcs1));
    return csrPem;
  }

  private static HttpClientHandler HttpMessageHandler(X509Certificate2Collection certificates)
  {
    var httpMessageHandler = new HttpClientHandler();
    httpMessageHandler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
    httpMessageHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
    httpMessageHandler.ClientCertificates.AddRange(certificates);
    return httpMessageHandler;
  }

  private static void SetupSimpleEnrollEndpoint(WireMockServer server, X509Certificate2 caCert)
  {
    server.Given(Request.Create().UsingPost().WithPath("/simpleenroll"))
      .RespondWith(Response.Create().WithCallback(message =>
      {
        using var key = RSA.Create(4096);
        var csrString = message.Body ?? throw new InvalidOperationException("Message body is null");
        var csr = CertificateRequest.LoadSigningRequestPem(csrString, HashAlgorithmName.SHA256);
        var deviceCert = csr.Create(
          caCert.IssuerName,
          X509SignatureGenerator.CreateForRSA(key, RSASignaturePadding.Pkcs1),
          DateTimeOffset.Now.AddDays(-1),
          DateTimeOffset.Now.AddYears(1),
          [1, 2, 3, 4]);
        var responseMessage = new ResponseMessage
        {
          StatusCode = 200,
          BodyData = new BodyData
          {
            BodyAsString = Convert.ToBase64String(deviceCert.Export(X509ContentType.Pkcs12)),
            DetectedBodyType = BodyType.String
          },
          Headers = new Dictionary<string, WireMockList<string>>
          {
            ["Content-Type"] = "application/pkcs12"
          }
        };
        return responseMessage;
      }));
  }

  private static void SetupCaCertEndpoint(WireMockServer server, X509Certificate2 caCert)
  {
    server.Given(Request.Create().UsingGet().WithPath("/cacert"))
      .RespondWith(Response.Create().WithCallback(message =>
      {
        var responseMessage = new ResponseMessage
        {
          StatusCode = 200,
          BodyData = new BodyData
          {
            BodyAsString = Convert.ToBase64String(caCert.Export(X509ContentType.Pfx)),
            DetectedBodyType = BodyType.String
          },
          Headers = new Dictionary<string, WireMockList<string>>
          {
            ["Content-Type"] = "application/pkcs12"
          }
        };
        return responseMessage;
      }));
  }

  private X509Certificate2 CreateSelfSignedCaCertificate(string commonName)
  {
    var subject = $"CN={commonName}";
    using var rsa = RSA.Create(4096);
    var request = new CertificateRequest(subject, rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

    request.CertificateExtensions.Add(
      new X509BasicConstraintsExtension(false, false, 0, true));

    var certificate = request.CreateSelfSigned(
      DateTimeOffset.Now.AddDays(-1),
      DateTimeOffset.Now.AddYears(1));

    return certificate;
  }
}