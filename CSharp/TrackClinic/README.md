# TrackClinic

TrackClinic is a service for managing song projects.

## Local Development

To run the service locally, you need a MongoDB instance. You can start one using Docker Compose:

```bash
docker compose up -d
```

Once the database is running, you can start the service using the .NET CLI:

```bash
cd TrackClinic
dotnet run --launch-profile http
```

The service will be available at `http://localhost:5168`.
You can access the API documentation (Scalar) at `http://localhost:5168/scalar/v1`.

## Testing

To run the tests, use the following command:

```bash
dotnet test
```

The tests use Testcontainers to automatically start a MongoDB instance, so you don't need to have one running manually for the tests.
