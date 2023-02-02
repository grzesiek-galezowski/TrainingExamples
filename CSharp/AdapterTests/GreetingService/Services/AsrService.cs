using Grpc.Core;
using NVidiaAsr;

namespace GreetingService.Services;

public class AsrService: RivaSpeechRecognition.RivaSpeechRecognitionBase
{
  public override Task<RecognizeResponse> Recognize(RecognizeRequest request, ServerCallContext context)
  {
    return base.Recognize(request, context);
  }

  public override Task StreamingRecognize(IAsyncStreamReader<StreamingRecognizeRequest> requestStream, IServerStreamWriter<StreamingRecognizeResponse> responseStream,
    ServerCallContext context)
  {
    return base.StreamingRecognize(requestStream, responseStream, context);
  }

  public override async Task<RivaSpeechRecognitionConfigResponse> GetRivaSpeechRecognitionConfig(RivaSpeechRecognitionConfigRequest request, ServerCallContext context)
  {
    return new RivaSpeechRecognitionConfigResponse()
    {
      ModelConfig =
      {
        new RivaSpeechRecognitionConfigResponse.Types.Config()
        {
          ModelName = "lol"
        }
      }
    };
  }
}