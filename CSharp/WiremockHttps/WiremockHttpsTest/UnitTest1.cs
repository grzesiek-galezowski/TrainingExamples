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
    var rsaKey = RSA.Create(4096);
    using var server = WireMockServer.Start(
      new WireMockServerSettings
      {
        ClientCertificateMode = ClientCertificateMode.RequireCertificate,
        AcceptAnyClientCertificate = true,
        UseSSL = true,
      });

    // Create a self-signed certificate for testing
    var caCert = CreateSelfSignedCertificate("WireMockTestCert");

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

    server.Given(Request.Create().UsingPost().WithPath("/simpleenroll"))
      .RespondWith(Response.Create().WithCallback(message =>
      {
        var csrString = message.Body;
        var csr = CertificateRequest.LoadSigningRequestPem(csrString, HashAlgorithmName.SHA256);
        var deviceCert = csr.Create(
          caCert.IssuerName,
          X509SignatureGenerator.CreateForRSA(rsaKey, RSASignaturePadding.Pkcs1),
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


    var certificates = X509CertificateLoader.LoadPkcs12Collection(
      caCert.Export(X509ContentType.Pfx),
      null,
      X509KeyStorageFlags.Exportable);

    var httpMessageHandler = new HttpClientHandler
    {
      ServerCertificateCustomValidationCallback = (_, _, _, _) => true,
      ClientCertificateOptions = ClientCertificateOption.Manual
    };
    httpMessageHandler.ClientCertificates.AddRange(certificates);

    // Act
    var response = await new HttpClient(httpMessageHandler)
      .GetAsync("https://localhost:" + server.Ports[0] + "/cacert")
      .ConfigureAwait(false);

    // Assert
    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    Assert.That(response.Content.Headers.GetValues("content-type").Single(), Is.EqualTo("application/pkcs12"));
    Assert.That(response.Content.ReadAsStringAsync().Result, Is.Not.EqualTo(""));

    var csr = new CertificateRequest(
      new X500DistinguishedName("CN=" + deviceId),
      rsaKey,
      HashAlgorithmName.SHA256,
      RSASignaturePadding.Pkcs1);
    var csrPem = csr.CreateSigningRequestPem(X509SignatureGenerator.CreateForRSA(rsaKey, RSASignaturePadding.Pkcs1));
    var enrollResponse = await new HttpClient(httpMessageHandler)
      .PostAsync("https://localhost:" + server.Ports[0] + "/simpleenroll", new StringContent(csrPem));
    Assert.That(enrollResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    Assert.That(response.Content.Headers.GetValues("content-type").Single(), Is.EqualTo("application/pkcs12"));
    Assert.That(response.Content.ReadAsStringAsync().Result, Is.Not.EqualTo(""));

  }

  private X509Certificate2 CreateSelfSignedCertificate(string commonName)
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