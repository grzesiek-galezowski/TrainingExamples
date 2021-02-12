using System.Threading.Tasks;

namespace OutsideInTdd.App
{
    public interface ITodoCommand
    {
        Task Execute();
    }
}