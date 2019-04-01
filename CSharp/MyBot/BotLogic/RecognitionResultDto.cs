using System.Collections.Generic;

namespace BotLogic
{
  public class RecognitionResultDto
  {
    public string Intent { get; set; }
    public IReadOnlyList<EntityDto> Entities { get; set; }
  }
}