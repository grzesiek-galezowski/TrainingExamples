namespace TodoApp1.Endpoints;

public interface IEndpoint
{
  Task Handle(HttpContext context);
}