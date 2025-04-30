using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CaptchaGenerator
{
    public partial class Form1 : Form
    {
        private string currentCaptchaAnswer = "";
        private readonly Random rand = new Random();
        private readonly Panel[,] colorGrid = new Panel[3, 3];
        private string targetColorName = "";
        private readonly HashSet<Panel> selectedPanels = new HashSet<Panel>();

        public Form1()
        {
            InitializeComponent();
            comboCaptchaType.SelectedIndex = 0;
            GenerateCaptcha();
        }

        private void GenerateCaptcha()
        {
            ClearColorGrid();
            selectedPanels.Clear();
            string selectedType = comboCaptchaType.SelectedItem?.ToString();
            switch (selectedType)
            {
                case "Text-Based":
                    currentCaptchaAnswer = GenerateTextCaptcha();
                    break;
                case "Math-Based":
                    currentCaptchaAnswer = GenerateMathCaptcha();
                    break;
                case "Image-Based":
                    GenerateColorGridCaptcha();
                    break;
                case "reCAPTCHA Checkbox":
                    captchaLabel.Text = "Please confirm you are not a robot.";
                    break;
                default:
                    captchaLabel.Text = "Select CAPTCHA Type";
                    break;
            }
        }

        private string GenerateTextCaptcha()
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            string captcha = string.Concat(Enumerable.Range(0, 6).Select(_ => chars[rand.Next(chars.Length)]));
            captchaLabel.Text = captcha;
            return captcha;
        }

        private string GenerateMathCaptcha()
        {
            int a = rand.Next(1, 20);
            int b = rand.Next(1, 20);
            captchaLabel.Text = $"Solve: {a} + {b} = ?";
            return (a + b).ToString();
        }

        private void GenerateColorGridCaptcha()
        {
            string[] colorNames = { "Red", "Green", "Blue", "Yellow", "Cyan", "Magenta", "Orange", "Purple", "Brown" };
            Color[] colors = { Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Cyan, Color.Magenta, Color.Orange, Color.Purple, Color.Brown };

            targetColorName = colorNames[rand.Next(colorNames.Length)];
            captchaLabel.Text = $"Select all squares with color: {targetColorName}";

            List<int> shuffledIndices = Enumerable.Range(0, 9).OrderBy(_ => rand.Next()).ToList();

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    int index = shuffledIndices[row * 3 + col];
                    var panel = new Panel
                    {
                        BackColor = colors[index % colors.Length],
                        Size = new Size(60, 60),
                        Location = new Point(300 + col * 65, 60 + row * 65),
                        BorderStyle = BorderStyle.FixedSingle,
                        Tag = colorNames[index % colorNames.Length],
                        Cursor = Cursors.Hand
                    };
                    panel.Click += ColorBox_Click;
                    panel.Paint += (s, e) => e.Graphics.DrawRectangle(new Pen(Color.DarkGray, 2), 1, 1, panel.Width - 2, panel.Height - 2);
                    this.Controls.Add(panel);
                    colorGrid[row, col] = panel;
                }
            }
        }

        private void ClearColorGrid()
        {
            foreach (Panel p in colorGrid)
            {
                if (p != null)
                    this.Controls.Remove(p);
            }
        }

        private void ColorBox_Click(object sender, EventArgs e)
        {
            Panel clickedPanel = sender as Panel;
            if (clickedPanel == null) return;

            if (selectedPanels.Contains(clickedPanel))
            {
                selectedPanels.Remove(clickedPanel);
                clickedPanel.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                selectedPanels.Add(clickedPanel);
                clickedPanel.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            userInput.Text = string.Empty;
            checkBoxRecaptcha.Checked = false;
            GenerateCaptcha();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string selectedType = comboCaptchaType.SelectedItem?.ToString();

            if (selectedType == "reCAPTCHA Checkbox")
            {
                MessageBox.Show(checkBoxRecaptcha.Checked
                    ? "reCAPTCHA verification successful!"
                    : "Please check the box to verify.",
                    checkBoxRecaptcha.Checked ? "Success" : "Error",
                    MessageBoxButtons.OK,
                    checkBoxRecaptcha.Checked ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            else if (selectedType == "Image-Based")
            {
                var correctPanels = colorGrid.Cast<Panel>().Where(p => (string)p.Tag == targetColorName);
                bool allCorrectSelected = correctPanels.All(p => selectedPanels.Contains(p));
                bool noIncorrectSelected = selectedPanels.All(p => (string)p.Tag == targetColorName);

                MessageBox.Show((allCorrectSelected && noIncorrectSelected)
                    ? "CAPTCHA validation successful!"
                    : "Incorrect selection. Please try again.",
                    (allCorrectSelected && noIncorrectSelected) ? "Success" : "Error",
                    MessageBoxButtons.OK,
                    (allCorrectSelected && noIncorrectSelected) ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            else if (userInput.Text.Trim().Equals(currentCaptchaAnswer, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("CAPTCHA validation successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Incorrect CAPTCHA. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GenerateCaptcha();
            }
        }

        private void comboCaptchaType_SelectedIndexChanged(object sender, EventArgs e)
        {
            userInput.Visible = checkBoxRecaptcha.Visible = false;

            if (comboCaptchaType.SelectedItem.ToString() == "reCAPTCHA Checkbox")
                checkBoxRecaptcha.Visible = true;
            else if (comboCaptchaType.SelectedItem.ToString() != "Image-Based")
                userInput.Visible = true;

            GenerateCaptcha();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
