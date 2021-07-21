using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WarehouseModule.AppLogic;

namespace ModularMonolith.UpdateOrder
{
  public class UpdateOrderStateResponseInProgress : IUpdateOrderStateResponseInProgress
  {
    private readonly HttpResponse _response;

    public UpdateOrderStateResponseInProgress(HttpResponse response)
    {
      _response = response;
    }

    public async Task Failure(Exception exception, CancellationToken cancellationToken)
    {
      _response.StatusCode = (int)HttpStatusCode.InternalServerError;
      await _response.WriteAsync("Something went wrong, but this version won't tell you what", cancellationToken);
    }

    public async Task Success(OrderDto orderDto, CancellationToken cancellationToken)
    {
      _response.StatusCode = (int)HttpStatusCode.Accepted;
      await _response.WriteAsJsonAsync(orderDto, cancellationToken);
    }
  }
}