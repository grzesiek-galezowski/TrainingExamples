using System.Threading.Tasks;

internal interface IIntent
{
  Task ApplyTo(DialogStateMachine dialogStateMachine, User user);
}