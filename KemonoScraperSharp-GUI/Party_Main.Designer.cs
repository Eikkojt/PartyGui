namespace KemonoScraperSharp_GUI
{
    partial class Party_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Party_Main));
            mainPanel = new Panel();
            label4 = new Label();
            parallelBox = new TextBox();
            label3 = new Label();
            chunksBox = new TextBox();
            writeDescCheck = new CheckBox();
            doNumbers = new CheckBox();
            outputLabel = new Label();
            urlLabel = new Label();
            descLabel = new Label();
            scrapeButton = new Button();
            postNumBox = new TextBox();
            postSubfoldersCheck = new CheckBox();
            urlBox = new TextBox();
            outputDirBox = new TextBox();
            outputDirButton = new Button();
            logLabel = new Label();
            individualProgressBar = new ProgressBar();
            nameLabel = new Label();
            pfpBox = new PictureBox();
            postProcessBar = new ProgressBar();
            translateDescCheck = new CheckBox();
            translateCheck = new CheckBox();
            passwordLabel = new Label();
            passwordBox = new TextBox();
            checkMegaSupport = new CheckBox();
            panel1 = new Panel();
            linkLabel1 = new LinkLabel();
            label1 = new Label();
            gifToggleCheck = new CheckBox();
            megaGifBox = new PictureBox();
            button1 = new Button();
            megaCmdBox = new TextBox();
            panel2 = new Panel();
            label2 = new Label();
            localeBox = new TextBox();
            panel3 = new Panel();
            logRichBox = new RichTextBox();
            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pfpBox).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)megaGifBox).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.BackColor = Color.WhiteSmoke;
            mainPanel.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Controls.Add(label4);
            mainPanel.Controls.Add(parallelBox);
            mainPanel.Controls.Add(label3);
            mainPanel.Controls.Add(chunksBox);
            mainPanel.Controls.Add(writeDescCheck);
            mainPanel.Controls.Add(doNumbers);
            mainPanel.Controls.Add(outputLabel);
            mainPanel.Controls.Add(urlLabel);
            mainPanel.Controls.Add(descLabel);
            mainPanel.Controls.Add(scrapeButton);
            mainPanel.Controls.Add(postNumBox);
            mainPanel.Controls.Add(postSubfoldersCheck);
            mainPanel.Controls.Add(urlBox);
            mainPanel.Controls.Add(outputDirBox);
            mainPanel.Controls.Add(outputDirButton);
            mainPanel.Location = new Point(12, 12);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(776, 250);
            mainPanel.TabIndex = 5;
            mainPanel.Click += panel1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(460, 116);
            label4.Name = "label4";
            label4.Size = new Size(207, 15);
            label4.TabIndex = 20;
            label4.Text = "Downloader Concurrent Connections:";
            // 
            // parallelBox
            // 
            parallelBox.Location = new Point(673, 111);
            parallelBox.Name = "parallelBox";
            parallelBox.PlaceholderText = "# Of Threads...";
            parallelBox.Size = new Size(100, 23);
            parallelBox.TabIndex = 19;
            parallelBox.TextAlign = HorizontalAlignment.Center;
            parallelBox.TextChanged += parallelBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(529, 91);
            label3.Name = "label3";
            label3.Size = new Size(138, 15);
            label3.TabIndex = 18;
            label3.Text = "Downloader File Chunks:";
            // 
            // chunksBox
            // 
            chunksBox.Location = new Point(673, 86);
            chunksBox.Name = "chunksBox";
            chunksBox.PlaceholderText = "# Of Chunks...";
            chunksBox.Size = new Size(100, 23);
            chunksBox.TabIndex = 17;
            chunksBox.TextAlign = HorizontalAlignment.Center;
            chunksBox.TextChanged += chunksBox_TextChanged;
            // 
            // writeDescCheck
            // 
            writeDescCheck.AutoSize = true;
            writeDescCheck.Location = new Point(84, 90);
            writeDescCheck.Name = "writeDescCheck";
            writeDescCheck.Size = new Size(148, 19);
            writeDescCheck.TabIndex = 16;
            writeDescCheck.Text = "Download Descriptions";
            writeDescCheck.UseVisualStyleBackColor = true;
            writeDescCheck.CheckedChanged += writeDescCheck_CheckedChanged;
            // 
            // doNumbers
            // 
            doNumbers.AutoSize = true;
            doNumbers.Location = new Point(84, 115);
            doNumbers.Name = "doNumbers";
            doNumbers.Size = new Size(158, 19);
            doNumbers.TabIndex = 11;
            doNumbers.Text = "Enable Subfolder # Suffix";
            doNumbers.UseVisualStyleBackColor = true;
            // 
            // outputLabel
            // 
            outputLabel.AutoSize = true;
            outputLabel.Location = new Point(366, 353);
            outputLabel.Name = "outputLabel";
            outputLabel.Size = new Size(0, 15);
            outputLabel.TabIndex = 10;
            outputLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // urlLabel
            // 
            urlLabel.AutoSize = true;
            urlLabel.Location = new Point(5, 35);
            urlLabel.Name = "urlLabel";
            urlLabel.Size = new Size(73, 15);
            urlLabel.TabIndex = 9;
            urlLabel.Text = "Creator URL:";
            // 
            // descLabel
            // 
            descLabel.AutoSize = true;
            descLabel.Location = new Point(566, 65);
            descLabel.Name = "descLabel";
            descLabel.Size = new Size(101, 15);
            descLabel.TabIndex = 8;
            descLabel.Text = "Number Of Posts:";
            // 
            // scrapeButton
            // 
            scrapeButton.Location = new Point(340, 222);
            scrapeButton.Name = "scrapeButton";
            scrapeButton.Size = new Size(95, 23);
            scrapeButton.TabIndex = 1;
            scrapeButton.Text = "Scrape Posts";
            scrapeButton.UseVisualStyleBackColor = true;
            scrapeButton.Click += scrapeButton_Click;
            // 
            // postNumBox
            // 
            postNumBox.Location = new Point(673, 61);
            postNumBox.Name = "postNumBox";
            postNumBox.PlaceholderText = "# Of Posts...";
            postNumBox.Size = new Size(100, 23);
            postNumBox.TabIndex = 6;
            postNumBox.TextAlign = HorizontalAlignment.Center;
            postNumBox.KeyDown += numberBox_EnterPressed;
            postNumBox.Leave += numberBox_FocusLost;
            // 
            // postSubfoldersCheck
            // 
            postSubfoldersCheck.AutoSize = true;
            postSubfoldersCheck.Checked = true;
            postSubfoldersCheck.CheckState = CheckState.Checked;
            postSubfoldersCheck.Location = new Point(84, 65);
            postSubfoldersCheck.Name = "postSubfoldersCheck";
            postSubfoldersCheck.Size = new Size(108, 19);
            postSubfoldersCheck.TabIndex = 4;
            postSubfoldersCheck.Text = "Post Subfolders";
            postSubfoldersCheck.UseVisualStyleBackColor = true;
            postSubfoldersCheck.CheckedChanged += postSubfoldersCheck_CheckedChanged;
            // 
            // urlBox
            // 
            urlBox.Location = new Point(84, 32);
            urlBox.Name = "urlBox";
            urlBox.PlaceholderText = "Place URL Here";
            urlBox.Size = new Size(689, 23);
            urlBox.TabIndex = 0;
            urlBox.TabStop = false;
            urlBox.TextAlign = HorizontalAlignment.Center;
            urlBox.KeyDown += urlBox_EnterPressed;
            urlBox.Leave += urlBox_FocusLost;
            // 
            // outputDirBox
            // 
            outputDirBox.Enabled = false;
            outputDirBox.Location = new Point(84, 3);
            outputDirBox.Name = "outputDirBox";
            outputDirBox.PlaceholderText = "Output Directory";
            outputDirBox.Size = new Size(689, 23);
            outputDirBox.TabIndex = 3;
            outputDirBox.TextAlign = HorizontalAlignment.Center;
            // 
            // outputDirButton
            // 
            outputDirButton.Location = new Point(3, 3);
            outputDirButton.Name = "outputDirButton";
            outputDirButton.Size = new Size(75, 23);
            outputDirButton.TabIndex = 2;
            outputDirButton.Text = "Browse...";
            outputDirButton.UseVisualStyleBackColor = true;
            outputDirButton.Click += outputDirButton_Click;
            // 
            // logLabel
            // 
            logLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            logLabel.Location = new Point(292, 250);
            logLabel.Name = "logLabel";
            logLabel.Size = new Size(190, 23);
            logLabel.TabIndex = 17;
            logLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // individualProgressBar
            // 
            individualProgressBar.Location = new Point(5, 276);
            individualProgressBar.Name = "individualProgressBar";
            individualProgressBar.Size = new Size(766, 23);
            individualProgressBar.TabIndex = 14;
            // 
            // nameLabel
            // 
            nameLabel.Location = new Point(307, 69);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(160, 15);
            nameLabel.TabIndex = 13;
            nameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pfpBox
            // 
            pfpBox.Location = new Point(307, 87);
            pfpBox.Name = "pfpBox";
            pfpBox.Size = new Size(160, 160);
            pfpBox.TabIndex = 12;
            pfpBox.TabStop = false;
            pfpBox.Click += pfpBox_Click;
            // 
            // postProcessBar
            // 
            postProcessBar.Location = new Point(5, 305);
            postProcessBar.Name = "postProcessBar";
            postProcessBar.Size = new Size(766, 23);
            postProcessBar.TabIndex = 7;
            // 
            // translateDescCheck
            // 
            translateDescCheck.AutoSize = true;
            translateDescCheck.Enabled = false;
            translateDescCheck.Location = new Point(3, 309);
            translateDescCheck.Name = "translateDescCheck";
            translateDescCheck.Size = new Size(140, 19);
            translateDescCheck.TabIndex = 15;
            translateDescCheck.Text = "Translate Descriptions";
            translateDescCheck.UseVisualStyleBackColor = true;
            translateDescCheck.CheckedChanged += translateDescCheck_CheckedChanged;
            // 
            // translateCheck
            // 
            translateCheck.AutoSize = true;
            translateCheck.Location = new Point(3, 284);
            translateCheck.Name = "translateCheck";
            translateCheck.Size = new Size(128, 19);
            translateCheck.TabIndex = 5;
            translateCheck.Text = "Translate Post Titles";
            translateCheck.UseVisualStyleBackColor = true;
            translateCheck.CheckedChanged += translateCheck_CheckedChanged;
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(225, 36);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(146, 15);
            passwordLabel.TabIndex = 19;
            passwordLabel.Text = "Creator's MEGA Password:";
            // 
            // passwordBox
            // 
            passwordBox.Enabled = false;
            passwordBox.Location = new Point(377, 32);
            passwordBox.Name = "passwordBox";
            passwordBox.PasswordChar = '*';
            passwordBox.PlaceholderText = "Password";
            passwordBox.Size = new Size(100, 23);
            passwordBox.TabIndex = 18;
            passwordBox.TextAlign = HorizontalAlignment.Center;
            passwordBox.UseSystemPasswordChar = true;
            // 
            // checkMegaSupport
            // 
            checkMegaSupport.AutoSize = true;
            checkMegaSupport.Location = new Point(164, 379);
            checkMegaSupport.Name = "checkMegaSupport";
            checkMegaSupport.Size = new Size(146, 19);
            checkMegaSupport.TabIndex = 17;
            checkMegaSupport.Text = "Download MEGA Links";
            checkMegaSupport.UseVisualStyleBackColor = true;
            checkMegaSupport.CheckedChanged += checkMegaSupport_CheckedChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.WhiteSmoke;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(linkLabel1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(gifToggleCheck);
            panel1.Controls.Add(megaGifBox);
            panel1.Controls.Add(checkMegaSupport);
            panel1.Controls.Add(passwordLabel);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(passwordBox);
            panel1.Controls.Add(megaCmdBox);
            panel1.Location = new Point(791, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(480, 426);
            panel1.TabIndex = 6;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            linkLabel1.Location = new Point(426, 404);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(49, 15);
            linkLabel1.TabIndex = 23;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Proxifier";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 404);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 22;
            label1.Text = "Please use proxies!";
            // 
            // gifToggleCheck
            // 
            gifToggleCheck.AutoSize = true;
            gifToggleCheck.Location = new Point(192, 400);
            gifToggleCheck.Name = "gifToggleCheck";
            gifToggleCheck.Size = new Size(84, 19);
            gifToggleCheck.TabIndex = 21;
            gifToggleCheck.Text = "Disable GIF";
            gifToggleCheck.UseVisualStyleBackColor = true;
            gifToggleCheck.CheckedChanged += gifToggleCheck_CheckedChanged;
            // 
            // megaGifBox
            // 
            megaGifBox.Anchor = AnchorStyles.Top;
            megaGifBox.Image = (Image)resources.GetObject("megaGifBox.Image");
            megaGifBox.Location = new Point(89, 63);
            megaGifBox.Name = "megaGifBox";
            megaGifBox.Size = new Size(300, 300);
            megaGifBox.SizeMode = PictureBoxSizeMode.Zoom;
            megaGifBox.TabIndex = 20;
            megaGifBox.TabStop = false;
            megaGifBox.Click += pictureBox1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 19;
            button1.Text = "Browse...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // megaCmdBox
            // 
            megaCmdBox.Enabled = false;
            megaCmdBox.Location = new Point(84, 3);
            megaCmdBox.Name = "megaCmdBox";
            megaCmdBox.PlaceholderText = "MegaCMD Install Directory";
            megaCmdBox.Size = new Size(393, 23);
            megaCmdBox.TabIndex = 18;
            megaCmdBox.TextAlign = HorizontalAlignment.Center;
            megaCmdBox.TextChanged += megaCmdBox_TextChanged;
            // 
            // panel2
            // 
            panel2.BackColor = Color.WhiteSmoke;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label2);
            panel2.Controls.Add(localeBox);
            panel2.Controls.Add(translateCheck);
            panel2.Controls.Add(translateDescCheck);
            panel2.Location = new Point(791, 444);
            panel2.Name = "panel2";
            panel2.Size = new Size(480, 333);
            panel2.TabIndex = 7;
            panel2.Paint += panel2_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(265, 6);
            label2.Name = "label2";
            label2.Size = new Size(104, 15);
            label2.TabIndex = 17;
            label2.Text = "Localization Code:";
            // 
            // localeBox
            // 
            localeBox.Enabled = false;
            localeBox.Location = new Point(375, 3);
            localeBox.Name = "localeBox";
            localeBox.PlaceholderText = "en";
            localeBox.Size = new Size(100, 23);
            localeBox.TabIndex = 16;
            localeBox.TextAlign = HorizontalAlignment.Center;
            localeBox.TextChanged += localeBox_TextChanged;
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(logLabel);
            panel3.Controls.Add(nameLabel);
            panel3.Controls.Add(postProcessBar);
            panel3.Controls.Add(individualProgressBar);
            panel3.Controls.Add(pfpBox);
            panel3.Location = new Point(12, 444);
            panel3.Name = "panel3";
            panel3.Size = new Size(776, 333);
            panel3.TabIndex = 8;
            panel3.Paint += panel3_Paint;
            // 
            // logRichBox
            // 
            logRichBox.Enabled = false;
            logRichBox.Location = new Point(12, 268);
            logRichBox.Name = "logRichBox";
            logRichBox.ReadOnly = true;
            logRichBox.Size = new Size(776, 170);
            logRichBox.TabIndex = 9;
            logRichBox.Text = "";
            logRichBox.TextChanged += richTextBox1_TextChanged;
            // 
            // Party_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1286, 789);
            Controls.Add(logRichBox);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Party_Main";
            Text = "Party Scraper";
            FormClosing += Party_Main_FormClosing;
            Load += Kemono_Main_Load;
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pfpBox).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)megaGifBox).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel mainPanel;
        private Label outputLabel;
        private Label urlLabel;
        private Label descLabel;
        private ProgressBar postProcessBar;
        private TextBox postNumBox;
        private CheckBox translateCheck;
        private CheckBox postSubfoldersCheck;
        private TextBox urlBox;
        private Button scrapeButton;
        private TextBox outputDirBox;
        private Button outputDirButton;
        private PictureBox pfpBox;
        private Label nameLabel;
        private ProgressBar individualProgressBar;
        private CheckBox translateDescCheck;
        private CheckBox writeDescCheck;
        private CheckBox checkMegaSupport;
        private TextBox passwordBox;
        private Label passwordLabel;
        private Panel panel1;
        private Button button1;
        private TextBox megaCmdBox;
        private PictureBox megaGifBox;
        private CheckBox gifToggleCheck;
        private LinkLabel linkLabel1;
        private Label label1;
        private CheckBox doNumbers;
        private Label logLabel;
        private Panel panel2;
        private Label label2;
        private TextBox localeBox;
        private Panel panel3;
        private RichTextBox logRichBox;
        private TextBox chunksBox;
        private Label label3;
        private Label label4;
        private TextBox parallelBox;
    }
}