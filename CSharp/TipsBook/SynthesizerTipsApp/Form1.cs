using System.Text;
using System.Text.RegularExpressions;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Linq;

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
                using (var pdfDoc = new PdfDocument(reader))
                {
                    var text = new StringBuilder();
                    
                    for (var page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
                    {
                        var strategy = new SimpleTextExtractionStrategy();
                        var currentText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page), strategy);
                        text.AppendLine(currentText);
                    }                    // Split text into paragraphs
                    var fullText = text.ToString();
                    
                    // First, try to extract all numbered items (digits followed by a dot and space)
                    var tipPattern = new Regex(@"(\d+)\.\s+([\s\S]*?)(?=\r?\n\d+\.|\z)", RegexOptions.Multiline);
                    var matches = tipPattern.Matches(fullText);
                    
                    foreach (Match match in matches)
                    {
                        if (match.Groups.Count >= 3)
                        {
                            var tipNumber = match.Groups[1].Value.Trim();
                            var tipContent = match.Groups[2].Value.Trim();
                            
                            // Clean up any extra whitespace
                            tipContent = Regex.Replace(tipContent, @"\s+\r?\n\s*", " ").Trim();
                            
                            // Format the tip with its number and content
                            var formattedTip = $"{tipNumber}. {tipContent}";
                            
                            // Only add if we have a valid tip number and some content
                            if (!string.IsNullOrWhiteSpace(tipNumber) && !string.IsNullOrWhiteSpace(tipContent))
                            {
                                paragraphs.Add(formattedTip);
                            }
                        }
                    }
                    
                    // If we still don't have all tips, try a more aggressive approach
                    if (paragraphs.Count < 10000) // If we're missing many tips
                    {
                        // Try splitting by line breaks and looking for numbered items
                        var lines = fullText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var line in lines)
                        {
                            var match = Regex.Match(line, @"^(\d+)\.\s*(.+)");
                            if (match.Success)
                            {
                                var tipNumber = match.Groups[1].Value.Trim();
                                var tipText = match.Groups[2].Value.Trim();
                                
                                if (!string.IsNullOrWhiteSpace(tipText) && !paragraphs.Any(p => p.StartsWith($"{tipNumber}. ")))
                                {
                                    paragraphs.Add($"{tipNumber}. {tipText}");
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
