var builder = DistributedApplication.CreateBuilder(args);

var mongodb = builder.AddMongoDB("mongodb")
  .WithDataVolume()
  .AddDatabase("TrackClinic");

var zipkin = builder.AddContainer("zipkin", "ghcr.io/openzipkin-contrib/zipkin-otel", "0.3.0")
  .WithHttpEndpoint(targetPort: 9411, port: 9411, name: "http");

var prometheus = builder.AddContainer("prometheus", "prom/prometheus", "latest")
  .WithHttpEndpoint(targetPort: 9090, port: 9090, name: "http")
  .WithBindMount(Path.Combine("..", "prometheus.yml"), "/etc/prometheus/prometheus.yml");

builder.AddProject<Projects.TrackClinic>("TrackClinicService")
  .WithReference(mongodb)
  .WithEnvironment("DatabaseInfo__ConnectionString", mongodb.Resource.ConnectionStringExpression)
  .WaitFor(mongodb)
  .WithReference(zipkin.GetEndpoint("http"))
  .WithEnvironment("OpenTelemetry__Endpoint", zipkin.GetEndpoint("http"))
  .WaitFor(zipkin);

builder.Build().Run();
