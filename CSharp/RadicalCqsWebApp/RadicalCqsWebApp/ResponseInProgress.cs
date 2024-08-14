using Microsoft.AspNetCore.Mvc;

namespace RadicalCqsWebApp;

public class ResponseInProgress
{
  private IResult? _result;

  public IResult ToResult()
  {
    return _result ?? Results.Problem(new ProblemDetails()
    {
      Status = 500,
      Title = "InternalServerError",
      Detail = "Something went wrong"
    });
  }

  public void Success(AddTodoResponseDto dto)
  {
    _result = Results.Ok(dto);
  }
}