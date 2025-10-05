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

    using var server = WireMockServer.Start(
      new WireMockServerSettings
      {
        ClientCertificateMode = ClientCertificateMode.RequireCertificate,
        AcceptAnyClientCertificate = true,
        UseSSL = true,
      });

    // Create a self-signed certificate for testing
    var cert = CreateSelfSignedCertificate("WireMockTestCert");

    server.Given(Request.Create().UsingGet().WithPath("/cacert*"))
      .RespondWith(Response.Create().WithCallback(message =>
      {
        var responseMessage = new ResponseMessage
        {
          StatusCode = message.ClientCertificate?.Thumbprint != null
            ? 200
            : 403,
          BodyData = new BodyData
          {
            BodyAsString = Convert.ToBase64String(cert.Export(X509ContentType.Pfx, "1234")),
            DetectedBodyType = BodyType.String
          }
        };
        responseMessage.Headers["Content-Type"] = "application/pkcs12";
        return responseMessage;
      }));


    var certificates = X509CertificateLoader.LoadPkcs12Collection(cert.Export(X509ContentType.Pfx, "1234"), "1234", X509KeyStorageFlags.Exportable);

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