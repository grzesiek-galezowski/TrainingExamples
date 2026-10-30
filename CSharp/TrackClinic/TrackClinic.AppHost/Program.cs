var builder = DistributedApplication.CreateBuilder(args);

var mongoPassword = builder.AddParameter("mongodb-password", value: "admin"); // not production use
var mongodb = builder.AddMongoDB("mongodb", password: mongoPassword)
  .WithDataVolume() // to mount a volume to persist data across container restarts
  .AddDatabase("TrackClinic");

var zipkin = builder.AddContainer("zipkin", "ghcr.io/openzipkin-contrib/zipkin-otel", "0.3.0")
  .WithHttpEndpoint(targetPort: 9411, port: 9411, name: "http");

var prometheus = builder.AddContainer("prometheus", "prom/prometheus", "latest")
  .WithHttpEndpoint(targetPort: 9090, port: 9090, name: "http")
  .WithBindMount(Path.Combine("..", "prometheus.yml"), "/etc/prometheus/prometheus.yml");

var grafana = builder.AddContainer("grafana", "grafana/grafana", "latest")
  .WithHttpEndpoint(targetPort: 3000, port: 3000, name: "http")
  .WithBindMount(Path.Combine("..", "grafana", "provisioning"), "/etc/grafana/provisioning")
  .WithEnvironment("GF_SECURITY_ADMIN_USER", "admin")
  .WithEnvironment("GF_SECURITY_ADMIN_PASSWORD", "admin")
  .WithEnvironment("GF_AUTH_ANONYMOUS_ENABLED", "true")
  .WithEnvironment("GF_AUTH_ANONYMOUS_ORG_ROLE", "Admin")
  .WaitFor(prometheus);

builder.AddProject<Projects.TrackClinic>("TrackClinicService")
  .WithReference(mongodb)
  .WithEnvironment("DatabaseInfo__ConnectionString", mongodb.Resource.ConnectionStringExpression)
  .WaitFor(mongodb)
  .WithReference(zipkin.GetEndpoint("http"))
  .WithEnvironment("OpenTelemetry__Endpoint", zipkin.GetEndpoint("http"))
  .WaitFor(zipkin);

builder.Build().Run();
