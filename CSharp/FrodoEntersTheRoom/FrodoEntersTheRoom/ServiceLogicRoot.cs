using TddXt.SimpleNlp;

namespace FrodoEntersTheRoom;

public class ServiceLogicRoot
{
  private readonly RecognitionModel _recognitionModel;

  public ServiceLogicRoot()
  {
    var recognitionModel = new RecognitionModel();
    recognitionModel.AddEntity("Character", "Gandalf", new[] {"gandalf"});
    recognitionModel.AddEntity("Kill", "Kill", new[] {"kill", "destroy", "attack"});
    recognitionModel.AddIntent("KillCharacter", new[] {"Kill", "Character"});
    _recognitionModel = recognitionModel;
  }

  public async Task Handle(HttpContext httpContext)
  {
    var body = await BodyFrom(httpContext);
    var recognitionResult = _recognitionModel.Recognize(body);
    var human = new ResponseCommunicatingThroughHttp(httpContext);
    var intent = IntentFactory.CreateIntentBasedOn(recognitionResult, human);
    await intent.Apply();
  }

  private static async Task<string> BodyFrom(HttpContext context)
  {
    using var streamReader = new StreamReader(context.Request.Body);
    return await streamReader.ReadToEndAsync();
  }
}