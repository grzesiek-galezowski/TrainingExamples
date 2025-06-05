using System.Text;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace SynthesizerTipsApp
{
    public partial class Form1 : Form
    {
        private string? pdfFilePath;
        private List<string> paragraphs = new List<string>();
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            Text = "Synthesizer Tips of the Day";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(600, 400);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ShowOpenFileDialog();
        }

        private void ShowOpenFileDialog()
        {
            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            openFileDialog.Title = "Select 1000 Tips for Synthesizers PDF";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pdfFilePath = openFileDialog.FileName;
                ExtractParagraphsFromPdf();
                ShowRandomTip();
            }
            else
            {
                MessageBox.Show("No PDF file selected. Please restart the application to select a file.", 
                    "File Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }

        private void ExtractParagraphsFromPdf()
        {
            if (string.IsNullOrEmpty(pdfFilePath))
            {
                MessageBox.Show("No PDF file path specified.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var reader = new PdfReader(pdfFilePath))
                {
                    var text = new StringBuilder();
                    
                    for (var page = 1; page <= reader.NumberOfPages; page++)
                    {
                        text.Append(PdfTextExtractor.GetTextFromPage(reader, page));
                    }                    // Split text into paragraphs
                    var fullText = text.ToString();
                    
                    // Pattern to match numbered tips with examples
                    // Looking for: number + period + text + "Example:" + text (until next number or end)
                    var tipPattern = new Regex(@"(\d+)\.\s+([^\r\n]+)[\r\n]+Example:\s+([^\r\n]+)", RegexOptions.Multiline);
                    
                    var matches = tipPattern.Matches(fullText);
                    
                    foreach (Match match in matches)
                    {
                        if (match.Groups.Count >= 4)
                        {
                            var tipNumber = match.Groups[1].Value.Trim();
                            var tipText = match.Groups[2].Value.Trim();
                            var exampleText = match.Groups[3].Value.Trim();
                            
                            // Format the tip with its number and example
                            var formattedTip = $"{tipNumber}. {tipText}\r\n\r\nExample: {exampleText}";
                            
                            if (!string.IsNullOrWhiteSpace(formattedTip))
                            {
                                paragraphs.Add(formattedTip);
                            }
                        }
                    }
                    
                    // If we didn't find any matches using the regex pattern,
                    // fall back to a simpler extraction method
                    if (paragraphs.Count == 0)
                    {
                        // Try to extract by looking for numbered lines
                        var fallbackPattern = new Regex(@"(\d+)\.\s+([^\r\n]+)", RegexOptions.Multiline);
                        var fallbackMatches = fallbackPattern.Matches(fullText);
                        
                        foreach (Match match in fallbackMatches)
                        {
                            if (match.Groups.Count >= 3)
                            {
                                var tipNumber = match.Groups[1].Value.Trim();
                                var tipText = match.Groups[2].Value.Trim();
                                
                                var formattedTip = $"{tipNumber}. {tipText}";
                                
                                if (!string.IsNullOrWhiteSpace(formattedTip) && formattedTip.Length > 10)
                                {
                                    paragraphs.Add(formattedTip);
                                }
                            }
                        }
                    }
                }

                if (paragraphs.Count == 0)
                {
                    MessageBox.Show("No tips found in the PDF. The extraction might not be working correctly with this PDF format.",
                                   "No Tips Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        private void ShowRandomTip()
        {
            if (paragraphs.Count == 0)
            {
                MessageBox.Show("No tips found in the PDF. Please select another PDF file.", 
                               "No Tips Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var tipIndex = random.Next(paragraphs.Count);
            var tipText = paragraphs[tipIndex];

            // Create a custom tip form and show it
            using var tipForm = new TipForm(tipText, tipIndex + 1, paragraphs.Count);
            var result = tipForm.ShowDialog(this);
                
            // Check the dialog result to determine what to do next
            if (result == DialogResult.Yes)  // "Next Tip" was clicked
            {
                ShowRandomTip();  // Show another random tip
            }
            else  // "Close" was clicked or form was closed by other means
            {
                Close();  // Close the app directly
            }
        }
    }
}
