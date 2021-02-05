using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OutsideInTdd.Adapters
{
    public interface ITodoEndpoint
    {
        Task HandleAsync(HttpContext context);
    }
}