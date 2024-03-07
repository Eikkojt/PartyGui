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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            creatorTextbox = new ReaLTaiizor.Controls.MaterialTextBoxEdit();
            materialLabel1 = new ReaLTaiizor.Controls.MaterialLabel();
            postNumberTextbox = new ReaLTaiizor.Controls.MaterialTextBoxEdit();
            materialLabel2 = new ReaLTaiizor.Controls.MaterialLabel();
            moduleOptionsPanel = new ReaLTaiizor.Controls.MaterialExpansionPanel();
            moduleConfigDivider = new ReaLTaiizor.Controls.MaterialDivider();
            subfoldersSwitch = new ReaLTaiizor.Controls.MaterialSwitch();
            downloadDescriptionsSwitch = new ReaLTaiizor.Controls.MaterialSwitch();
            translationOptionsPanel = new ReaLTaiizor.Controls.MaterialExpansionPanel();
            scrapeButton = new ReaLTaiizor.Controls.MaterialButton();
            materialExpansionPanel1 = new ReaLTaiizor.Controls.MaterialExpansionPanel();
            probeCreatorButton = new ReaLTaiizor.Controls.MaterialButton();
            probePanel = new ReaLTaiizor.Controls.MaterialExpansionPanel();
            probeUploadTimeLabel = new ReaLTaiizor.Controls.MaterialLabel();
            probeAuthorLabel = new ReaLTaiizor.Controls.MaterialLabel();
            probeCommentLabel = new ReaLTaiizor.Controls.MaterialLabel();
            probeAttachmentLabel = new ReaLTaiizor.Controls.MaterialLabel();
            probeNameLabel = new ReaLTaiizor.Controls.MaterialLabel();
            attachmentsList = new ImageList(components);
            translateTitlesSwitch = new ReaLTaiizor.Controls.MaterialSwitch();
            translateDescriptionsSwitch = new ReaLTaiizor.Controls.MaterialSwitch();
            materialTextBoxEdit1 = new ReaLTaiizor.Controls.MaterialTextBoxEdit();
            materialLabel3 = new ReaLTaiizor.Controls.MaterialLabel();
            materialDivider1 = new ReaLTaiizor.Controls.MaterialDivider();
            moduleOptionsPanel.SuspendLayout();
            translationOptionsPanel.SuspendLayout();
            materialExpansionPanel1.SuspendLayout();
            probePanel.SuspendLayout();
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
            creatorTextbox.Location = new Point(133, 64);
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
            creatorTextbox.Size = new Size(557, 64);
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
            materialLabel1.Location = new Point(27, 64);
            materialLabel1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(100, 48);
            materialLabel1.TabIndex = 1;
            materialLabel1.Text = "Creator:";
            materialLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // postNumberTextbox
            // 
            postNumberTextbox.AnimateReadOnly = false;
            postNumberTextbox.AutoCompleteMode = AutoCompleteMode.None;
            postNumberTextbox.AutoCompleteSource = AutoCompleteSource.None;
            postNumberTextbox.BackgroundImageLayout = ImageLayout.None;
            postNumberTextbox.CharacterCasing = CharacterCasing.Normal;
            postNumberTextbox.Depth = 0;
            postNumberTextbox.ErrorMessage = "Invalid number!";
            postNumberTextbox.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            postNumberTextbox.HideSelection = true;
            postNumberTextbox.LeadingIcon = null;
            postNumberTextbox.Location = new Point(133, 134);
            postNumberTextbox.MaxLength = 32767;
            postNumberTextbox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            postNumberTextbox.Name = "postNumberTextbox";
            postNumberTextbox.PasswordChar = '\0';
            postNumberTextbox.PrefixSuffixText = null;
            postNumberTextbox.ReadOnly = false;
            postNumberTextbox.RightToLeft = RightToLeft.No;
            postNumberTextbox.SelectedText = "";
            postNumberTextbox.SelectionLength = 0;
            postNumberTextbox.SelectionStart = 0;
            postNumberTextbox.ShortcutsEnabled = true;
            postNumberTextbox.ShowAssistiveText = true;
            postNumberTextbox.Size = new Size(118, 64);
            postNumberTextbox.TabIndex = 2;
            postNumberTextbox.TabStop = false;
            postNumberTextbox.TextAlign = HorizontalAlignment.Center;
            postNumberTextbox.TrailingIcon = null;
            postNumberTextbox.UseSystemPasswordChar = false;
            postNumberTextbox.TextChanged += postNumberTextbox_TextChanged;
            // 
            // materialLabel2
            // 
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel2.Location = new Point(27, 134);
            materialLabel2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(100, 48);
            materialLabel2.TabIndex = 3;
            materialLabel2.Text = "Posts:";
            materialLabel2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // moduleOptionsPanel
            // 
            moduleOptionsPanel.BackColor = Color.FromArgb(255, 255, 255);
            moduleOptionsPanel.Controls.Add(moduleConfigDivider);
            moduleOptionsPanel.Controls.Add(subfoldersSwitch);
            moduleOptionsPanel.Controls.Add(downloadDescriptionsSwitch);
            moduleOptionsPanel.Controls.Add(postNumberTextbox);
            moduleOptionsPanel.Controls.Add(materialLabel2);
            moduleOptionsPanel.Controls.Add(materialLabel1);
            moduleOptionsPanel.Controls.Add(creatorTextbox);
            moduleOptionsPanel.Depth = 0;
            moduleOptionsPanel.Description = "Properties relating to core information needed by PartyModule";
            moduleOptionsPanel.ExpandHeight = 436;
            moduleOptionsPanel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            moduleOptionsPanel.ForeColor = Color.FromArgb(222, 0, 0, 0);
            moduleOptionsPanel.Location = new Point(12, 25);
            moduleOptionsPanel.Margin = new Padding(16);
            moduleOptionsPanel.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            moduleOptionsPanel.Name = "moduleOptionsPanel";
            moduleOptionsPanel.Padding = new Padding(24, 64, 24, 16);
            moduleOptionsPanel.ShowValidationButtons = false;
            moduleOptionsPanel.Size = new Size(717, 436);
            moduleOptionsPanel.TabIndex = 4;
            moduleOptionsPanel.Title = "Module Config";
            moduleOptionsPanel.UseAccentColor = true;
            moduleOptionsPanel.Click += ReleaseKeyboard;
            // 
            // moduleConfigDivider
            // 
            moduleConfigDivider.BackColor = Color.FromArgb(30, 0, 0, 0);
            moduleConfigDivider.Depth = 0;
            moduleConfigDivider.Location = new Point(27, 204);
            moduleConfigDivider.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            moduleConfigDivider.Name = "moduleConfigDivider";
            moduleConfigDivider.Size = new Size(663, 23);
            moduleConfigDivider.TabIndex = 6;
            moduleConfigDivider.Text = "materialDivider1";
            // 
            // subfoldersSwitch
            // 
            subfoldersSwitch.AutoSize = true;
            subfoldersSwitch.Checked = true;
            subfoldersSwitch.CheckState = CheckState.Checked;
            subfoldersSwitch.Depth = 0;
            subfoldersSwitch.Location = new Point(27, 230);
            subfoldersSwitch.Margin = new Padding(0);
            subfoldersSwitch.MouseLocation = new Point(-1, -1);
            subfoldersSwitch.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            subfoldersSwitch.Name = "subfoldersSwitch";
            subfoldersSwitch.Ripple = true;
            subfoldersSwitch.Size = new Size(169, 37);
            subfoldersSwitch.TabIndex = 5;
            subfoldersSwitch.Text = "Post subfolders";
            subfoldersSwitch.UseAccentColor = true;
            subfoldersSwitch.UseVisualStyleBackColor = true;
            subfoldersSwitch.CheckedChanged += subfolderSwitch_CheckedChanged;
            // 
            // downloadDescriptionsSwitch
            // 
            downloadDescriptionsSwitch.AutoSize = true;
            downloadDescriptionsSwitch.Depth = 0;
            downloadDescriptionsSwitch.Location = new Point(27, 267);
            downloadDescriptionsSwitch.Margin = new Padding(0);
            downloadDescriptionsSwitch.MouseLocation = new Point(-1, -1);
            downloadDescriptionsSwitch.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            downloadDescriptionsSwitch.Name = "downloadDescriptionsSwitch";
            downloadDescriptionsSwitch.Ripple = true;
            downloadDescriptionsSwitch.Size = new Size(255, 37);
            downloadDescriptionsSwitch.TabIndex = 4;
            downloadDescriptionsSwitch.Text = "Download post descriptions";
            downloadDescriptionsSwitch.UseAccentColor = false;
            downloadDescriptionsSwitch.UseVisualStyleBackColor = true;
            downloadDescriptionsSwitch.CheckedChanged += downloadDescriptionsSwitch_CheckedChanged;
            // 
            // translationOptionsPanel
            // 
            translationOptionsPanel.BackColor = Color.FromArgb(255, 255, 255);
            translationOptionsPanel.Controls.Add(materialDivider1);
            translationOptionsPanel.Controls.Add(materialLabel3);
            translationOptionsPanel.Controls.Add(materialTextBoxEdit1);
            translationOptionsPanel.Controls.Add(translateDescriptionsSwitch);
            translationOptionsPanel.Controls.Add(translateTitlesSwitch);
            translationOptionsPanel.Depth = 0;
            translationOptionsPanel.Description = "Properties relating to the built-in translation service";
            translationOptionsPanel.ExpandHeight = 354;
            translationOptionsPanel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            translationOptionsPanel.ForeColor = Color.FromArgb(222, 0, 0, 0);
            translationOptionsPanel.Location = new Point(12, 493);
            translationOptionsPanel.Margin = new Padding(3, 16, 3, 16);
            translationOptionsPanel.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            translationOptionsPanel.Name = "translationOptionsPanel";
            translationOptionsPanel.Padding = new Padding(24, 64, 24, 16);
            translationOptionsPanel.ShowValidationButtons = false;
            translationOptionsPanel.Size = new Size(717, 354);
            translationOptionsPanel.TabIndex = 5;
            translationOptionsPanel.Title = "Translation Config";
            translationOptionsPanel.Click += ReleaseKeyboard;
            // 
            // scrapeButton
            // 
            scrapeButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            scrapeButton.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            scrapeButton.Depth = 0;
            scrapeButton.HighEmphasis = true;
            scrapeButton.Icon = null;
            scrapeButton.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            scrapeButton.Location = new Point(28, 70);
            scrapeButton.Margin = new Padding(4, 6, 4, 6);
            scrapeButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            scrapeButton.Name = "scrapeButton";
            scrapeButton.NoAccentTextColor = Color.Empty;
            scrapeButton.Size = new Size(123, 36);
            scrapeButton.TabIndex = 6;
            scrapeButton.Text = "Begin Scrape";
            scrapeButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            scrapeButton.UseAccentColor = false;
            scrapeButton.UseVisualStyleBackColor = true;
            // 
            // materialExpansionPanel1
            // 
            materialExpansionPanel1.BackColor = Color.FromArgb(255, 255, 255);
            materialExpansionPanel1.Controls.Add(probeCreatorButton);
            materialExpansionPanel1.Controls.Add(scrapeButton);
            materialExpansionPanel1.Depth = 0;
            materialExpansionPanel1.Description = "This is where the magic happens";
            materialExpansionPanel1.ExpandHeight = 436;
            materialExpansionPanel1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialExpansionPanel1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialExpansionPanel1.Location = new Point(748, 25);
            materialExpansionPanel1.Margin = new Padding(3, 16, 3, 16);
            materialExpansionPanel1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialExpansionPanel1.Name = "materialExpansionPanel1";
            materialExpansionPanel1.Padding = new Padding(24, 64, 24, 16);
            materialExpansionPanel1.ShowValidationButtons = false;
            materialExpansionPanel1.Size = new Size(442, 436);
            materialExpansionPanel1.TabIndex = 7;
            materialExpansionPanel1.Title = "Execution";
            materialExpansionPanel1.Click += ReleaseKeyboard;
            // 
            // probeCreatorButton
            // 
            probeCreatorButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            probeCreatorButton.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            probeCreatorButton.Depth = 0;
            probeCreatorButton.HighEmphasis = true;
            probeCreatorButton.Icon = null;
            probeCreatorButton.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            probeCreatorButton.Location = new Point(277, 70);
            probeCreatorButton.Margin = new Padding(4, 6, 4, 6);
            probeCreatorButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            probeCreatorButton.Name = "probeCreatorButton";
            probeCreatorButton.NoAccentTextColor = Color.Empty;
            probeCreatorButton.Size = new Size(137, 36);
            probeCreatorButton.TabIndex = 7;
            probeCreatorButton.Text = "Probe Creator";
            probeCreatorButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            probeCreatorButton.UseAccentColor = false;
            probeCreatorButton.UseVisualStyleBackColor = true;
            probeCreatorButton.Click += probeCreatorButton_Click;
            // 
            // probePanel
            // 
            probePanel.BackColor = Color.FromArgb(255, 255, 255);
            probePanel.Controls.Add(probeUploadTimeLabel);
            probePanel.Controls.Add(probeAuthorLabel);
            probePanel.Controls.Add(probeCommentLabel);
            probePanel.Controls.Add(probeAttachmentLabel);
            probePanel.Controls.Add(probeNameLabel);
            probePanel.Depth = 0;
            probePanel.Description = "Glorified bug tester";
            probePanel.ExpandHeight = 200;
            probePanel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            probePanel.ForeColor = Color.FromArgb(222, 0, 0, 0);
            probePanel.Location = new Point(1196, 25);
            probePanel.Margin = new Padding(3, 16, 3, 16);
            probePanel.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            probePanel.Name = "probePanel";
            probePanel.Padding = new Padding(24, 64, 24, 16);
            probePanel.ShowValidationButtons = false;
            probePanel.Size = new Size(371, 200);
            probePanel.TabIndex = 8;
            probePanel.Title = "Probe Info";
            probePanel.Click += ReleaseKeyboard;
            // 
            // probeUploadTimeLabel
            // 
            probeUploadTimeLabel.Depth = 0;
            probeUploadTimeLabel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            probeUploadTimeLabel.Location = new Point(27, 156);
            probeUploadTimeLabel.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            probeUploadTimeLabel.Name = "probeUploadTimeLabel";
            probeUploadTimeLabel.Size = new Size(308, 23);
            probeUploadTimeLabel.TabIndex = 9;
            probeUploadTimeLabel.Text = "Upload Date: ";
            probeUploadTimeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // probeAuthorLabel
            // 
            probeAuthorLabel.Depth = 0;
            probeAuthorLabel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            probeAuthorLabel.Location = new Point(27, 133);
            probeAuthorLabel.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            probeAuthorLabel.Name = "probeAuthorLabel";
            probeAuthorLabel.Size = new Size(308, 23);
            probeAuthorLabel.TabIndex = 6;
            probeAuthorLabel.Text = "Author: ";
            probeAuthorLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // probeCommentLabel
            // 
            probeCommentLabel.Depth = 0;
            probeCommentLabel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            probeCommentLabel.Location = new Point(27, 110);
            probeCommentLabel.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            probeCommentLabel.Name = "probeCommentLabel";
            probeCommentLabel.Size = new Size(308, 23);
            probeCommentLabel.TabIndex = 5;
            probeCommentLabel.Text = "Comments: ";
            probeCommentLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // probeAttachmentLabel
            // 
            probeAttachmentLabel.Depth = 0;
            probeAttachmentLabel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            probeAttachmentLabel.Location = new Point(27, 87);
            probeAttachmentLabel.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            probeAttachmentLabel.Name = "probeAttachmentLabel";
            probeAttachmentLabel.Size = new Size(308, 23);
            probeAttachmentLabel.TabIndex = 4;
            probeAttachmentLabel.Text = "Attachments: ";
            probeAttachmentLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // probeNameLabel
            // 
            probeNameLabel.Depth = 0;
            probeNameLabel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            probeNameLabel.Location = new Point(27, 64);
            probeNameLabel.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            probeNameLabel.Name = "probeNameLabel";
            probeNameLabel.Size = new Size(308, 23);
            probeNameLabel.TabIndex = 3;
            probeNameLabel.Text = "Name: ";
            probeNameLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // attachmentsList
            // 
            attachmentsList.ColorDepth = ColorDepth.Depth32Bit;
            attachmentsList.ImageSize = new Size(32, 32);
            attachmentsList.TransparentColor = Color.Transparent;
            // 
            // translateTitlesSwitch
            // 
            translateTitlesSwitch.AutoSize = true;
            translateTitlesSwitch.Depth = 0;
            translateTitlesSwitch.Location = new Point(24, 163);
            translateTitlesSwitch.Margin = new Padding(0);
            translateTitlesSwitch.MouseLocation = new Point(-1, -1);
            translateTitlesSwitch.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            translateTitlesSwitch.Name = "translateTitlesSwitch";
            translateTitlesSwitch.Ripple = true;
            translateTitlesSwitch.Size = new Size(198, 37);
            translateTitlesSwitch.TabIndex = 2;
            translateTitlesSwitch.Text = "Translate post titles";
            translateTitlesSwitch.UseAccentColor = false;
            translateTitlesSwitch.UseVisualStyleBackColor = true;
            // 
            // translateDescriptionsSwitch
            // 
            translateDescriptionsSwitch.AutoSize = true;
            translateDescriptionsSwitch.Depth = 0;
            translateDescriptionsSwitch.Location = new Point(24, 200);
            translateDescriptionsSwitch.Margin = new Padding(0);
            translateDescriptionsSwitch.MouseLocation = new Point(-1, -1);
            translateDescriptionsSwitch.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            translateDescriptionsSwitch.Name = "translateDescriptionsSwitch";
            translateDescriptionsSwitch.Ripple = true;
            translateDescriptionsSwitch.Size = new Size(250, 37);
            translateDescriptionsSwitch.TabIndex = 3;
            translateDescriptionsSwitch.Text = "Translate post descriptions";
            translateDescriptionsSwitch.UseAccentColor = false;
            translateDescriptionsSwitch.UseVisualStyleBackColor = true;
            // 
            // materialTextBoxEdit1
            // 
            materialTextBoxEdit1.AnimateReadOnly = false;
            materialTextBoxEdit1.AutoCompleteMode = AutoCompleteMode.None;
            materialTextBoxEdit1.AutoCompleteSource = AutoCompleteSource.None;
            materialTextBoxEdit1.BackgroundImageLayout = ImageLayout.None;
            materialTextBoxEdit1.CharacterCasing = CharacterCasing.Normal;
            materialTextBoxEdit1.Depth = 0;
            materialTextBoxEdit1.ErrorMessage = "Invalid localization code!";
            materialTextBoxEdit1.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialTextBoxEdit1.HideSelection = true;
            materialTextBoxEdit1.LeadingIcon = null;
            materialTextBoxEdit1.Location = new Point(133, 67);
            materialTextBoxEdit1.MaxLength = 32767;
            materialTextBoxEdit1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            materialTextBoxEdit1.Name = "materialTextBoxEdit1";
            materialTextBoxEdit1.PasswordChar = '\0';
            materialTextBoxEdit1.PrefixSuffixText = null;
            materialTextBoxEdit1.ReadOnly = false;
            materialTextBoxEdit1.RightToLeft = RightToLeft.No;
            materialTextBoxEdit1.SelectedText = "";
            materialTextBoxEdit1.SelectionLength = 0;
            materialTextBoxEdit1.SelectionStart = 0;
            materialTextBoxEdit1.ShortcutsEnabled = true;
            materialTextBoxEdit1.ShowAssistiveText = true;
            materialTextBoxEdit1.Size = new Size(118, 64);
            materialTextBoxEdit1.TabIndex = 4;
            materialTextBoxEdit1.TabStop = false;
            materialTextBoxEdit1.TextAlign = HorizontalAlignment.Center;
            materialTextBoxEdit1.TrailingIcon = null;
            materialTextBoxEdit1.UseSystemPasswordChar = false;
            // 
            // materialLabel3
            // 
            materialLabel3.Depth = 0;
            materialLabel3.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel3.Location = new Point(27, 67);
            materialLabel3.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialLabel3.Name = "materialLabel3";
            materialLabel3.Size = new Size(100, 48);
            materialLabel3.TabIndex = 7;
            materialLabel3.Text = "Language:";
            materialLabel3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // materialDivider1
            // 
            materialDivider1.BackColor = Color.FromArgb(30, 0, 0, 0);
            materialDivider1.Depth = 0;
            materialDivider1.Location = new Point(27, 137);
            materialDivider1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialDivider1.Name = "materialDivider1";
            materialDivider1.Size = new Size(663, 23);
            materialDivider1.TabIndex = 8;
            materialDivider1.Text = "materialDivider1";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1579, 872);
            Controls.Add(probePanel);
            Controls.Add(materialExpansionPanel1);
            Controls.Add(translationOptionsPanel);
            Controls.Add(moduleOptionsPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PartyScraper v2.0.0-alpha";
            FormClosing += Main_FormClosing;
            Load += Main_Load;
            Click += ReleaseKeyboard;
            moduleOptionsPanel.ResumeLayout(false);
            moduleOptionsPanel.PerformLayout();
            translationOptionsPanel.ResumeLayout(false);
            translationOptionsPanel.PerformLayout();
            materialExpansionPanel1.ResumeLayout(false);
            materialExpansionPanel1.PerformLayout();
            probePanel.ResumeLayout(false);
            probePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.MaterialTextBoxEdit creatorTextbox;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel1;
        private ReaLTaiizor.Controls.MaterialTextBoxEdit postNumberTextbox;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel2;
        private ReaLTaiizor.Controls.MaterialExpansionPanel moduleOptionsPanel;
        private ReaLTaiizor.Controls.MaterialExpansionPanel translationOptionsPanel;
        private ReaLTaiizor.Controls.MaterialSwitch downloadDescriptionsSwitch;
        private ReaLTaiizor.Controls.MaterialSwitch subfoldersSwitch;
        private ReaLTaiizor.Controls.MaterialDivider moduleConfigDivider;
        private ReaLTaiizor.Controls.MaterialButton scrapeButton;
        private ReaLTaiizor.Controls.MaterialExpansionPanel materialExpansionPanel1;
        private ReaLTaiizor.Controls.MaterialButton probeCreatorButton;
        private ReaLTaiizor.Controls.MaterialExpansionPanel probePanel;
        private ReaLTaiizor.Controls.MaterialLabel probeNameLabel;
        private ReaLTaiizor.Controls.MaterialLabel probeAttachmentLabel;
        private ReaLTaiizor.Controls.MaterialLabel probeCommentLabel;
        private ImageList attachmentsList;
        private ReaLTaiizor.Controls.MaterialLabel probeAuthorLabel;
        private ReaLTaiizor.Controls.MaterialLabel probeUploadTimeLabel;
        private ReaLTaiizor.Controls.MaterialSwitch translateTitlesSwitch;
        private ReaLTaiizor.Controls.MaterialSwitch translateDescriptionsSwitch;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel3;
        private ReaLTaiizor.Controls.MaterialTextBoxEdit materialTextBoxEdit1;
        private ReaLTaiizor.Controls.MaterialDivider materialDivider1;
    }
}
