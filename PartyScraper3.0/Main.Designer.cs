namespace PartyScraper3._0
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            creatorTextbox = new ReaLTaiizor.Controls.MaterialTextBoxEdit();
            materialLabel1 = new ReaLTaiizor.Controls.MaterialLabel();
            SuspendLayout();
            // 
            // creatorTextbox
            // 
            creatorTextbox.AnimateReadOnly = false;
            creatorTextbox.AutoCompleteMode = AutoCompleteMode.None;
            creatorTextbox.AutoCompleteSource = AutoCompleteSource.None;
            creatorTextbox.BackgroundImageLayout = ImageLayout.None;
            creatorTextbox.CharacterCasing = CharacterCasing.Normal;
            creatorTextbox.Depth = 0;
            creatorTextbox.ErrorMessage = "Please input a valid creator URL!";
            creatorTextbox.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            creatorTextbox.HideSelection = true;
            creatorTextbox.LeadingIcon = null;
            creatorTextbox.Location = new Point(118, 12);
            creatorTextbox.MaxLength = 32767;
            creatorTextbox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            creatorTextbox.Name = "creatorTextbox";
            creatorTextbox.PasswordChar = '\0';
            creatorTextbox.PrefixSuffixText = null;
            creatorTextbox.ReadOnly = false;
            creatorTextbox.RightToLeft = RightToLeft.No;
            creatorTextbox.SelectedText = "";
            creatorTextbox.SelectionLength = 0;
            creatorTextbox.SelectionStart = 0;
            creatorTextbox.ShortcutsEnabled = true;
            creatorTextbox.ShowAssistiveText = true;
            creatorTextbox.Size = new Size(1095, 64);
            creatorTextbox.TabIndex = 0;
            creatorTextbox.TabStop = false;
            creatorTextbox.TextAlign = HorizontalAlignment.Center;
            creatorTextbox.TrailingIcon = null;
            creatorTextbox.UseSystemPasswordChar = false;
            creatorTextbox.TextChanged += creatorTextbox_TextChanged;
            // 
            // materialLabel1
            // 
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel1.Location = new Point(12, 12);
            materialLabel1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(100, 48);
            materialLabel1.TabIndex = 1;
            materialLabel1.Text = "Creator:";
            materialLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1225, 872);
            Controls.Add(materialLabel1);
            Controls.Add(creatorTextbox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PartyScraper v2.0.0-alpha";
            Load += Main_Load;
            Click += ReleaseKeyboard;
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.MaterialTextBoxEdit creatorTextbox;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel1;
    }
}
