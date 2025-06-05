using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SynthesizerTipsApp
{
    public class TipForm : Form
    {
        private readonly string _tipText;
        private readonly int _tipNumber;
        private readonly int _totalTips;
        
        public TipForm(string tipText, int tipNumber, int totalTips)
        {
            _tipText = tipText;
            _tipNumber = tipNumber;
            _totalTips = totalTips;
            
            InitializeUI();
        }
        
        private void InitializeUI()
        {
            // Form settings
            this.Text = "Synthesizer Tip of the Day";
            this.Size = new Size(550, 350);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 240, 250); // Light blue-gray
            
            // Create title label
            Label titleLabel = new Label
            {
                Text = $"Tip #{_tipNumber} of {_totalTips}",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40,
                ForeColor = Color.FromArgb(50, 50, 150) // Dark blue
            };
            
            // Create panel for the tip text
            Panel tipPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };
            tipPanel.Paint += TipPanel_Paint;
            
            // Create the tip text label
            Label tipLabel = new Label
            {
                Text = _tipText,
                Font = new Font("Segoe UI", 11),
                Dock = DockStyle.Fill,
                AutoSize = false,
                TextAlign = ContentAlignment.TopLeft
            };
            
            // Create a container panel with padding
            Panel containerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15)
            };
            
            // Create button panel
            Panel buttonPanel = new Panel
            {
                Height = 50,
                Dock = DockStyle.Bottom
            };
            
            // Create close button
            Button closeButton = new Button
            {
                Text = "Close",
                Size = new Size(100, 30),
                Location = new Point((buttonPanel.Width - 100) / 2, (buttonPanel.Height - 30) / 2),
                Anchor = AnchorStyles.None,
                BackColor = Color.FromArgb(70, 70, 170),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            closeButton.Click += (s, e) => this.Close();
            
            // Add controls to the form
            buttonPanel.Controls.Add(closeButton);
            tipPanel.Controls.Add(tipLabel);
            containerPanel.Controls.Add(tipPanel);
            
            this.Controls.Add(buttonPanel);
            this.Controls.Add(containerPanel);
            this.Controls.Add(titleLabel);
            
            // Center the close button
            this.Shown += (s, e) =>
            {
                closeButton.Location = new Point((buttonPanel.Width - closeButton.Width) / 2, 
                                                (buttonPanel.Height - closeButton.Height) / 2);
            };
        }
        
        private void TipPanel_Paint(object sender, PaintEventArgs e)
        {
            // Create a fancy border/background for the tip panel
            Panel panel = sender as Panel;
            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            
            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = 10;
                path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
                path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
                path.CloseAllFigures();
                
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    rect, Color.FromArgb(255, 255, 255), Color.FromArgb(235, 235, 245), 
                    LinearGradientMode.Vertical))
                {
                    e.Graphics.FillPath(brush, path);
                }
                
                using (Pen pen = new Pen(Color.FromArgb(180, 180, 220), 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
}
