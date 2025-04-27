using System.Threading.Tasks;

namespace OutsideInTdd.App
{
    public interface IAddTodoResponseInProgress
    {
        Task ReportFailureBecauseOfInappropriateWord(string inappropriateWord);
    }
}