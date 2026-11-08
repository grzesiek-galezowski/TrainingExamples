var builder = DistributedApplication.CreateBuilder(args);

var realm = "track-clinic";

var keycloak = CreateKeycloak(builder);
var keycloakBase = keycloak.GetEndpoint("http").EndpointAnnotation;
var authority = $"{keycloakBase.Transport}://{keycloakBase.TargetHost}:{keycloakBase.Port}/realms/{realm}";
var metadata = $"{authority}/.well-known/openid-configuration";

var zipkin = CreateZipkinOtel(builder);
var prometheus = CreatePrometheus(builder);
var grafana = CreateGrafana(builder, prometheus, zipkin);
var trackClinic = CreateTrackClinic(builder, zipkin, authority, metadata, keycloak);
var demoUi = CreateDemoUi(builder, trackClinic, zipkin, authority, metadata, realm, keycloak);

builder.Build().Run();
return;

static IResourceBuilder<ProjectResource> CreateTrackClinic(
  IDistributedApplicationBuilder builder,
  IResourceBuilder<ContainerResource> resourceBuilder,
  string authority,
  string metadata,
  IResourceBuilder<ContainerResource> keycloak)
{
  var mongoPassword = builder.AddParameter("mongodb-password", value: "admin"); // not production use
  var mongodb = builder.AddMongoDB("mongodb", password: mongoPassword)
    .WithDataVolume() // to mount a volume to persist data across container restarts
    .AddDatabase("TrackClinic");
  var trackClinic = builder.AddProject<Projects.TrackClinic>("TrackClinicService")
    .WithReference(mongodb)
    .WithReference(resourceBuilder.GetEndpoint("http"))
    .WithHttpHealthCheck("/health/ready")
    .WithEnvironment("DatabaseInfo__ConnectionString", mongodb.Resource.ConnectionStringExpression)
    .WithEnvironment("Keycloak__Enabled", "true")
    .WithEnvironment("Keycloak__Authority", authority)
    .WithEnvironment("Keycloak__MetadataAddress", metadata)
    .WithEnvironment("Keycloak__RequireHttpsMetadata", "false")
    .WithEnvironment("OpenTelemetry__Endpoint", resourceBuilder.GetEndpoint("http"))
    .WaitFor(keycloak)
    .WaitFor(mongodb)
    .WaitFor(resourceBuilder);
  return trackClinic;
}

static IResourceBuilder<ContainerResource> CreateGrafana(IDistributedApplicationBuilder builder, IResourceBuilder<ContainerResource> prometheus, IResourceBuilder<ContainerResource> zipkin)
{
  return builder.AddContainer("grafana", "grafana/grafana", "latest")
    .WithHttpEndpoint(targetPort: 3000, port: 3000, name: "http")
    .WithBindMount(Path.Combine("..", "grafana", "provisioning"), "/etc/grafana/provisioning")
    .WithEnvironment("GF_SECURITY_ADMIN_USER", "admin")
    .WithEnvironment("GF_SECURITY_ADMIN_PASSWORD", "admin")
    .WithEnvironment("GF_AUTH_ANONYMOUS_ENABLED", "true")
    .WithEnvironment("GF_AUTH_ANONYMOUS_ORG_ROLE", "Admin")
    .WithEnvironment("PROMETHEUS_URL", prometheus.GetEndpoint("http"))
    .WithEnvironment("ZIPKIN_URL", zipkin.GetEndpoint("http"))
    .WithReference(prometheus.GetEndpoint("http")) //bug needed?
    .WithReference(zipkin.GetEndpoint("http")) //bug needed?
    .WaitFor(prometheus)
    .WaitFor(zipkin);
}

static IResourceBuilder<ProjectResource> CreateDemoUi(
  IDistributedApplicationBuilder distributedApplicationBuilder,
  IResourceBuilder<ProjectResource> trackClinic,
  IResourceBuilder<ContainerResource> zipkin,
  string authority,
  string metadata,
  string realm,
  IResourceBuilder<ContainerResource> keycloak)
{
  return distributedApplicationBuilder.AddProject<Projects.DemoUi>("DemoUi")
    .WithReference(trackClinic)
    .WithReference(zipkin.GetEndpoint("http"))
    .WithHttpHealthCheck("/health/ready")
    .WithEnvironment("OpenTelemetry__Endpoint", zipkin.GetEndpoint("http"))
    .WithEnvironment("Keycloak__Authority", authority)
    .WithEnvironment("Keycloak__MetadataAddress", metadata)
    .WithEnvironment("Keycloak__RequireHttpsMetadata", "false")
    .WithEnvironment("Keycloak__Realm", realm)
    .WithEnvironment("Keycloak__ClientId", "track-clinic-api")
    .WithEnvironment("Keycloak__Username", "artist-user")
    .WithEnvironment("Keycloak__Password", "password")
    .WaitFor(trackClinic)
    .WaitFor(keycloak);
}

static IResourceBuilder<ContainerResource> CreatePrometheus(IDistributedApplicationBuilder builder)
{
  return builder.AddContainer("prometheus", "prom/prometheus", "latest")
    .WithHttpEndpoint(targetPort: 9090, port: 9090, name: "http")
    .WithBindMount(Path.Combine("..", "prometheus.yml"), "/etc/prometheus/prometheus.yml");
}

static IResourceBuilder<ContainerResource> CreateZipkinOtel(IDistributedApplicationBuilder builder)
{
  return builder.AddContainer("zipkin", "ghcr.io/openzipkin-contrib/zipkin-otel", "0.3.0")
    .WithHttpEndpoint(targetPort: 9411, port: 9411, name: "http")
    .WithEnvironment("MEM_MAX_SPANS", "100000");
}

static IResourceBuilder<ContainerResource> CreateKeycloak(IDistributedApplicationBuilder builder)
{
  var resourceBuilder = builder.AddContainer("keycloak", "quay.io/keycloak/keycloak", "26.0")
    .WithHttpEndpoint(targetPort: 8080, port: 8080, name: "http")
    .WithEnvironment("KEYCLOAK_ADMIN", "admin")
    .WithEnvironment("KEYCLOAK_ADMIN_PASSWORD", "admin")
    .WithBindMount(
      Path.Combine("..", "keycloak", "track-clinic-realm.json"),
      "/opt/keycloak/data/import/track-clinic-realm.json")
    .WithArgs("start-dev", "--import-realm");
  return resourceBuilder;
}
