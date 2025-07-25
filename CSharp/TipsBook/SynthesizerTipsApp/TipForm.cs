using System.Drawing.Drawing2D;

namespace SynthesizerTipsApp;

public class TipForm : Form
{
  private readonly string _tipText;
  private readonly int _tipNumber;
  private readonly int _totalTips;
        
  public TipForm(string tipText, int tipNumber, int totalTips)
  {
    this._tipText = tipText;
    this._tipNumber = tipNumber;
    this._totalTips = totalTips;
            
    InitializeUi();
  }
        
  private void InitializeUi()
  {
    // Form settings
    Text = "Synthesizer Tip of the Day";
    Size = new Size(550, 350);
    FormBorderStyle = FormBorderStyle.FixedDialog;
    MaximizeBox = false;
    StartPosition = FormStartPosition.CenterParent;
    BackColor = Color.FromArgb(240, 240, 250); // Light blue-gray
            
    // Create title label
    var titleLabel = new Label
    {
      Text = $"Tip #{_tipNumber} of {_totalTips}",
      Font = new Font("Segoe UI", 14, FontStyle.Bold),
      TextAlign = ContentAlignment.MiddleCenter,
      Dock = DockStyle.Top,
      Height = 40,
      ForeColor = Color.FromArgb(50, 50, 150) // Dark blue
    };
            
    // Create panel for the tip text
    var tipPanel = new Panel
    {
      Dock = DockStyle.Fill,
      Padding = new Padding(20)
    };            tipPanel.Paint += TipPanel_Paint;
            
    // Parse the tip text to separate the tip and example
    var tipTitle = _tipText;
    var example = string.Empty;
            
    var exampleIndex = _tipText.IndexOf("Example:", StringComparison.OrdinalIgnoreCase);
    if (exampleIndex > 0)
    {
      tipTitle = _tipText[..exampleIndex].Trim();
      example = _tipText[exampleIndex..].Trim();
    }
            
    // Create the tip title label
    var tipTitleLabel = new Label
    {
      Text = tipTitle,
      Font = new Font("Segoe UI", 16, FontStyle.Regular),
      AutoSize = false,
      Dock = DockStyle.Top,
      Height = 70,
      TextAlign = ContentAlignment.TopLeft
    };
            
    // Create example label (if applicable)
    var exampleLabel = new Label
    {
      Text = example,
      Font = new Font("Segoe UI", 15, FontStyle.Italic),
      ForeColor = Color.FromArgb(80, 80, 160), // Dark blue for example text
      AutoSize = false,
      Dock = DockStyle.Fill,
      TextAlign = ContentAlignment.TopLeft
    };
            
    // Create a container panel with padding
    var containerPanel = new Panel
    {
      Dock = DockStyle.Fill,
      Padding = new Padding(15)
    };
            
    // Create button panel
    var buttonPanel = new Panel
    {
      Height = 50,
      Dock = DockStyle.Bottom
    };
    // Create next tip button
    var nextTipButton = new Button
    {
      Text = "Next Tip",
      Size = new Size(120, 30),
      Location = new Point(0, (buttonPanel.Height - 30) / 2),
      Anchor = AnchorStyles.None,
      BackColor = Color.FromArgb(70, 130, 180), // Steel Blue
      ForeColor = Color.White,
      FlatStyle = FlatStyle.Flat
    };
            
    // Create close button
    var closeButton = new Button
    {
      Text = "Close",
      Size = new Size(120, 30),
      Location = new Point(0, (buttonPanel.Height - 30) / 2),
      Anchor = AnchorStyles.None,
      BackColor = Color.FromArgb(70, 70, 170), // Dark Blue
      ForeColor = Color.White,
      FlatStyle = FlatStyle.Flat
    };
            
    // Set up event handlers
    nextTipButton.Click += (s, e) => 
    {
      DialogResult = DialogResult.Yes; // Use Yes as signal for "Next Tip"
      Close();
    };
            
    closeButton.Click += (_, _) => 
    {
      DialogResult = DialogResult.No; // Use No as signal for "Close App"
      Close();
    };            // Add controls to the form
    buttonPanel.Controls.Add(nextTipButton);
    buttonPanel.Controls.Add(closeButton);
    tipPanel.Controls.Add(exampleLabel);
    tipPanel.Controls.Add(tipTitleLabel);
    containerPanel.Controls.Add(tipPanel);
            
    Controls.Add(buttonPanel);
    Controls.Add(containerPanel);
    Controls.Add(titleLabel);
            
    // Position the buttons side by side
    Shown += (s, e) =>
    {
      var centerX = buttonPanel.Width / 2;
      nextTipButton.Location = new Point(centerX - nextTipButton.Width - 5, (buttonPanel.Height - nextTipButton.Height) / 2);
      closeButton.Location = new Point(centerX + 5, (buttonPanel.Height - closeButton.Height) / 2);
    };
  }

  private void TipPanel_Paint(object? sender, PaintEventArgs e)
  {
    // Create a fancy border/background for the tip panel
    var panel = sender as Panel;
            
    if (panel == null)
      return;
                
    var rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);

    using var path = new GraphicsPath();
    const int radius = 10;
    path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
    path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
    path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
    path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
    path.CloseAllFigures();
                
    using (var brush = new LinearGradientBrush(
             rect, Color.FromArgb(255, 255, 255), Color.FromArgb(235, 235, 245), 
             LinearGradientMode.Vertical))
    {
      e.Graphics.FillPath(brush, path);
    }
                
    using (var pen = new Pen(Color.FromArgb(180, 180, 220), 1))
    {
      e.Graphics.DrawPath(pen, path);
    }
  }
}