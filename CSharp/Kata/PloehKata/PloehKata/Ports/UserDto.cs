using System.Collections.Generic;

namespace PloehKata.Ports
{
    public class UserDto
    {
      public List<UserDto> Connections { get; private set; } = new List<UserDto>();
      public string Id { get; set; }
      public string Name { get; set; }
    }
}