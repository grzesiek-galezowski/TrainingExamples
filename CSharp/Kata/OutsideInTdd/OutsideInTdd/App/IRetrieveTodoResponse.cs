using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutsideInTdd.App
{
    public interface IRetrieveTodoResponse
    {
        Task ReportRetrievedData(List<TodoNoteDto> allItems);
    }
}