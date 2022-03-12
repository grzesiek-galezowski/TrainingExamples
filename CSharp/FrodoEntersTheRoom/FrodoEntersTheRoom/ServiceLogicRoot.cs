using TddXt.SimpleNlp;

namespace FrodoEntersTheRoom;

public class ServiceLogicRoot
{
  private readonly RecognitionModel _recognitionModel;

  public ServiceLogicRoot()
  {
    var recognitionModel = new RecognitionModel();
    var entityName = "Character";
    var entityName2 = "Kill";
    recognitionModel.AddEntity(entityName, "Gandalf", new[] {"gandalf"});
    recognitionModel.AddEntity(entityName2, "Kill", new[] {"kill", "destroy", "attack"});
    recognitionModel.AddIntent("KillCharacter", new[] {entityName2, entityName});
    _recognitionModel = recognitionModel;
  }

  public async Task Handle(HttpContext httpContext)
  {
    var body = await BodyFrom(httpContext);
    var recognitionResult = _recognitionModel.Recognize(body);
    var response = new HttpBasedResponse(httpContext);
    var intent = IntentFactory.CreateIntentBasedOn(recognitionResult, response);
    await intent.Apply();
  }

  private static async Task<string> BodyFrom(HttpContext context)
  {
    using var streamReader = new StreamReader(context.Request.Body);
    return await streamReader.ReadToEndAsync();
  }
}