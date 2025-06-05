using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
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
        this.Text = "Synthesizer Tips of the Day";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Size = new System.Drawing.Size(600, 400);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ShowOpenFileDialog();
    }

    private void ShowOpenFileDialog()
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
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
                this.Close();
            }
        }
    }

    private void ExtractParagraphsFromPdf()
    {
        try
        {
            using (PdfReader reader = new PdfReader(pdfFilePath))
            {
                StringBuilder text = new StringBuilder();
                
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, page));
                }

                // Split text into paragraphs
                string fullText = text.ToString();
                
                // Simple paragraph separation - adjust the regex pattern based on the actual PDF format
                string[] extractedParagraphs = Regex.Split(fullText, @"(?<=\.)\s+(?=[A-Z])");
                
                foreach (string paragraph in extractedParagraphs)
                {
                    string trimmed = paragraph.Trim();
                    if (!string.IsNullOrWhiteSpace(trimmed) && trimmed.Length > 20) // Filter out very short segments
                    {
                        paragraphs.Add(trimmed);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error reading PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ShowRandomTip()
    {
        if (paragraphs.Count == 0)
        {
            MessageBox.Show("No tips found in the PDF. Please select another PDF file.", 
                           "No Tips Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        int tipIndex = random.Next(paragraphs.Count);
        string tipText = paragraphs[tipIndex];

        // Create a custom tip form and show it
        using (var tipForm = new TipForm(tipText, tipIndex + 1, paragraphs.Count))
        {
            tipForm.ShowDialog(this);
            
            // When the tip form closes, ask if the user wants to see another tip
            if (MessageBox.Show("Would you like to see another tip?", "Another Tip?", 
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ShowRandomTip();
            }
            else
            {
                this.Close();
            }        }
    }
}
