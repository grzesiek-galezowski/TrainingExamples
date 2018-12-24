using System.Threading.Tasks;

namespace Bot_Builder_Echo_Bot_V4
{
  public interface IStatesFactory
  {
    IState GetState(States state);
  }
}