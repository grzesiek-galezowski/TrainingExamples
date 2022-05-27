using System.Text;
using Lib;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

public static class SpeechService
{
  public static async Task<string> MicrophoneSpeechToText()
  {
    var yourSubscriptionKey = await SecretStore.ReadSpeechServiceSubscriptionKey();
    var yourServiceRegion = await SecretStore.ReadSpeechServiceRegion();
    var speechConfig = SpeechConfig.FromSubscription(yourSubscriptionKey, yourServiceRegion);
    speechConfig.SpeechRecognitionLanguage = "en-US";
    using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
    using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

    var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
    var recognizedText = GetRecognizedText(speechRecognitionResult);
    return recognizedText;
  }

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


}

class Program 
{
    static string YourSubscriptionKey = "YourSubscriptionKey";
    static string YourServiceRegion = "YourServiceRegion";

    static void OutputSpeechSynthesisResult(SpeechSynthesisResult speechSynthesisResult, string text)
    {
        switch (speechSynthesisResult.Reason)
        {
            case ResultReason.SynthesizingAudioCompleted:
                Console.WriteLine($"Speech synthesized for text: [{text}]");
                break;
            case ResultReason.Canceled:
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);
                Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                    Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
                }
                break;
            default:
                break;
        }
    }

    async static Task Main(string[] args)
    {
        var speechConfig = SpeechConfig.FromSubscription(YourSubscriptionKey, YourServiceRegion);      
        speechConfig.SpeechSynthesisVoiceName = "en-US-JennyNeural"; 

        using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
        {
            // Get text from the console and synthesize to the default speaker.
            Console.WriteLine("Enter some text that you want to speak >");
            string text = Console.ReadLine();

            var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
            OutputSpeechSynthesisResult(speechSynthesisResult, text);
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}