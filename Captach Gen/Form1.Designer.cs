namespace CaptchaGenerator
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox comboCaptchaType;
        private Label captchaLabel;
        private TextBox userInput;
        private Button btnSubmit;
        private Button btnRefresh;
        private CheckBox checkBoxRecaptcha;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.comboCaptchaType = new ComboBox();
            this.captchaLabel = new Label();
            this.userInput = new TextBox();
            this.btnSubmit = new Button();
            this.btnRefresh = new Button();
            this.checkBoxRecaptcha = new CheckBox();

            this.SuspendLayout();

            // comboCaptchaType
            this.comboCaptchaType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboCaptchaType.Items.AddRange(new object[] {
                "Text-Based",
                "Image-Based",
                "Math-Based",
                "reCAPTCHA Checkbox"});
            this.comboCaptchaType.Location = new System.Drawing.Point(30, 20);
            this.comboCaptchaType.Size = new System.Drawing.Size(250, 24);
            this.comboCaptchaType.SelectedIndexChanged += new System.EventHandler(this.comboCaptchaType_SelectedIndexChanged);

            // captchaLabel
            this.captchaLabel.Location = new System.Drawing.Point(30, 60);
            this.captchaLabel.Size = new System.Drawing.Size(250, 40);
            this.captchaLabel.Font = new Font("Arial", 12, FontStyle.Bold);

            // userInput
            this.userInput.Location = new System.Drawing.Point(30, 110);
            this.userInput.Size = new System.Drawing.Size(200, 22);

            // checkBoxRecaptcha
            this.checkBoxRecaptcha.Location = new System.Drawing.Point(30, 110);
            this.checkBoxRecaptcha.Size = new System.Drawing.Size(200, 24);
            this.checkBoxRecaptcha.Text = "I'm not a robot";

            // btnSubmit
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.Location = new System.Drawing.Point(30, 150);
            this.btnSubmit.Size = new System.Drawing.Size(75, 30);
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);

            // btnRefresh
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Location = new System.Drawing.Point(120, 150);
            this.btnRefresh.Size = new System.Drawing.Size(75, 30);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // Form1
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.Controls.Add(this.comboCaptchaType);
            this.Controls.Add(this.captchaLabel);
            this.Controls.Add(this.userInput);
            this.Controls.Add(this.checkBoxRecaptcha);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnRefresh);
            this.Text = "CAPTCHA Generator";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
