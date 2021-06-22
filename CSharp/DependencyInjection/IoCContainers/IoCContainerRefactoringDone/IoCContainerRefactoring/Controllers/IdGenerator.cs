using System;

namespace IoCContainerRefactoring.Controllers
{
  public interface IIdGenerator
  {
    Guid NewId();
  }

  public class IdGenerator : IIdGenerator
  {
    public Guid NewId()
    {
      return Guid.NewGuid();
    }
  }
}