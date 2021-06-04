using System;

namespace IoCContainerRefactoring.Controllers
{
  internal class IdGenerator
  {
    public Guid NewId()
    {
      return Guid.NewGuid();
    }
  }
}