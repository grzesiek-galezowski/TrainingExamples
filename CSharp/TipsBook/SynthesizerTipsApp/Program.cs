namespace SynthesizerTipsApp;

static class Program
{
  /// <summary>
  ///  The main entry point for the application.
  /// </summary>
  [STAThread]
  private static void Main()
  {
    // To customize application configuration such as set high DPI settings or default font,
    // see https://aka.ms/applicationconfiguration.
    ApplicationConfiguration.Initialize();

    try
    {
      Application.Run(new Form1());
    }
    catch (Exception ex)
    {
      MessageBox.Show($"An unexpected error occurred: {ex.Message}\n\nPlease restart the application.",
        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
  }

}

public class Lol
{
  public void Write(params string[] text) => Console.WriteLine(text);
}