using System.Text;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using NAudio.Wave;
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
          var stringBuilder = new StringBuilder();
          var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
          stringBuilder.Append($"CANCELED: Reason={cancellation.Reason}"));

          if (cancellation.Reason == CancellationReason.Error)
          {
            stringBuilder.Append($"CANCELED: ErrorCode={cancellation.ErrorCode}");
            stringBuilder.Append($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
            stringBuilder.Append($"CANCELED: Did you set the speech resource key and region values?");
          }

          throw new Exception(stringBuilder.ToString());
      }
    }

    async static Task Main(string[] args)
    {
      var yourSubscriptionKey = await File.ReadAllTextAsync(
        $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\Documents\\__KEYS\\subscriptionKey.txt");
      var yourServiceRegion = await File.ReadAllTextAsync(
        $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\Documents\\__KEYS\\serviceRegion.txt");
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
    public static async Task Lol()
    {
      //WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(0);
      Console.WriteLine("Now recording...");
      WaveInEvent waveSource = new WaveInEvent();
      //waveSource.DeviceNumber = 0;
      waveSource.WaveFormat = new WaveFormat(44100, 1);

      waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);

      string tempFile = (@"C:\Users\user\Desktop\test1.wav");
      await using var waveFile = new WaveFileWriter(tempFile, waveSource.WaveFormat);
      waveSource.StartRecording();
      Console.WriteLine("Press enter to stop");
      Console.ReadLine();
      waveSource.StopRecording();
      waveFile.Dispose();
    }

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

            var carMake = FindCar(queryData);

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

    private static void waveSource_DataAvailable(object? sender, WaveInEventArgs e)
    {
      Console.WriteLine(e.BytesRecorded);
    }

    private static LicensePlateQueryData GetEntitiesFrom(Prediction data)
    {
      return new LicensePlateQueryData(
        ((JArray)data.Entities["PlateNumber"]).First.ToString(),
        ((JArray)data.Entities["State"]).First.ToString());
    }

    private static async Task<Prediction> GetStructuredOutputFrom(string text)
    {
      var luisKey = await File.ReadAllTextAsync($"{Environment.SpecialFolder.MyDocuments}\\__KEYS\\luis.txt");
      var luisAppId = await File.ReadAllTextAsync($"{Environment.SpecialFolder.MyDocuments}\\__KEYS\\luisApp.txt");
      var luisAppUrl = await File.ReadAllTextAsync($"{Environment.SpecialFolder.MyDocuments}\\__KEYS\\luisAppUrl.txt");
      var credentials = new ApiKeyServiceClientCredentials(luisKey);
      var runtimeClient 
        = new LUISRuntimeClient(credentials) { Endpoint = luisAppUrl };
      var result = await runtimeClient.Prediction.GetSlotPredictionWithHttpMessagesAsync(
        Guid.Parse(luisAppId), 
        "staging", 
        new PredictionRequest(text));

      return result.Body.Prediction;
    }

    private static string FindCar(LicensePlateQueryData queryData)
    {
      return queryData switch
      {
        { State: "Alabama", Number: "ABC1234" } => "Alfa Romeo",
        { State: "Alabama", Number: "ABC1235" } => "Cadillac",
        { State: "New York", Number: "ABC12" } => "Rolls Royce",
        _ => "Unknown"
      };
    }
  }

  public record LicensePlateQueryData(string Number, string State);
}