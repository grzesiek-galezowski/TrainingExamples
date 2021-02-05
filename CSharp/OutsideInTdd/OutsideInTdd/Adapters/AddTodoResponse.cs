using Microsoft.AspNetCore.Http;
using OutsideInTdd.App;

namespace OutsideInTdd.Adapters
{
    public class AddTodoResponse : IAddTodoResponse
    {
        public AddTodoResponse(HttpContext context)
        {
            
        }

        public void ReportFailureBecauseOfInappropriateWord(string inappropriateWord)
        {
            throw new System.NotImplementedException();
        }
    }
}