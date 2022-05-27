using System.Text;
using Lib;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Newtonsoft.Json.Linq;

namespace _03_AudioOutput
{
  class Program
  {
    private static string GetRecognizedText(
      RecognitionResult speechRecognitionResult)
    {
      switch (speechRecognitionResult.Reason)
      {
        case ResultReason.RecognizedSpeech:
          return speechRecognitionResult.Text;
        case ResultReason.NoMatch:
          throw new Exception("NOMATCH: Speech could not be recognized.");
        case ResultReason.Canceled:
        {
          var stringBuilder = new StringBuilder();
          var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
          stringBuilder.Append($"CANCELED: Reason={cancellation.Reason}");

          if (cancellation.Reason == CancellationReason.Error)
          {
            stringBuilder.Append($"CANCELED: ErrorCode={cancellation.ErrorCode}");
            stringBuilder.Append($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
            stringBuilder.Append($"CANCELED: Did you set the speech resource key and region values?");
          }

          throw new Exception(stringBuilder.ToString());
        }
        case ResultReason.RecognizingSpeech:
        case ResultReason.RecognizingIntent:
        case ResultReason.RecognizedIntent:
        case ResultReason.TranslatingSpeech:
        case ResultReason.TranslatedSpeech:
        case ResultReason.SynthesizingAudio:
        case ResultReason.SynthesizingAudioCompleted:
        case ResultReason.RecognizingKeyword:
        case ResultReason.RecognizedKeyword:
        case ResultReason.SynthesizingAudioStarted:
        case ResultReason.TranslatingParticipantSpeech:
        case ResultReason.TranslatedParticipantSpeech:
        case ResultReason.TranslatedInstantMessage:
        case ResultReason.TranslatedParticipantInstantMessage:
        case ResultReason.EnrollingVoiceProfile:
        case ResultReason.EnrolledVoiceProfile:
        case ResultReason.RecognizedSpeakers:
        case ResultReason.RecognizedSpeaker:
        case ResultReason.ResetVoiceProfile:
        case ResultReason.DeletedVoiceProfile:
        case ResultReason.VoicesListRetrieved:
        default:
          throw new InvalidOperationException("Unexpected reason " + speechRecognitionResult.Reason);
      }
    }

    public static async Task Main(string[] args)
    {
      var yourSubscriptionKey = await SecretStore.ReadSpeechServiceSubscriptionKey();
      var yourServiceRegion = await SecretStore.ReadSpeechServiceRegion();
      var speechConfig = SpeechConfig.FromSubscription(yourSubscriptionKey, yourServiceRegion);
      speechConfig.SpeechRecognitionLanguage = "en-US";

      using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
      using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

      Console.WriteLine("Speak into your microphone.");
      var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
      var recognizedText = GetRecognizedText(speechRecognitionResult);
      Console.WriteLine(recognizedText);
    }
  }

  public static class Program2
  {
    public static async Task Main2(string[] args)
    {
      while (true)
      {
        try
        {
          var text = Console.ReadLine();
          var data = await GetStructuredOutputFrom(text);

          if (data.TopIntent == "Plate")
          {
            var queryData = GetEntitiesFrom(data);

            var carMake = FederalDatabase.FindCar(queryData);

            Console.WriteLine(
              "The car with " +
              $"license plate: {queryData.Number} " +
              $"in state {queryData.State} " +
              $"is {carMake}");
          }
          else
          {
            Console.WriteLine("Sorry, I don't know what you mean");
          }
        }
        catch(Exception e)
        {
          Console.WriteLine("Invalid input!");
          Console.WriteLine(e.Message);
        }
      }
    }

    private static LicensePlateQueryData GetEntitiesFrom(Prediction data)
    {
      return new LicensePlateQueryData(
        ((JArray)data.Entities["PlateNumber"]).First.ToString(),
        ((JArray)data.Entities["State"]).First.ToString());
    }

    private static async Task<Prediction> GetStructuredOutputFrom(string text)
    {
      var luisKey = await SecretStore.ReadLuisKey();
      var luisAppId = await SecretStore.ReadLuisApp();
      var luisAppUrl = await SecretStore.ReadLuisUrl();
      var credentials = new ApiKeyServiceClientCredentials(luisKey);
      var runtimeClient 
        = new LUISRuntimeClient(credentials) { Endpoint = luisAppUrl };
      var result = await runtimeClient.Prediction.GetSlotPredictionWithHttpMessagesAsync(
        Guid.Parse(luisAppId), 
        "staging", 
        new PredictionRequest(text));

      return result.Body.Prediction;
    }
  }
}