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
            zipExtractCheck = new CheckBox();
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
            testApiButton = new Button();
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
            megaPanel = new Panel();
            killMegaButton = new Button();
            linkLabel1 = new LinkLabel();
            label1 = new Label();
            megaGifBox = new PictureBox();
            button1 = new Button();
            megaCmdBox = new TextBox();
            gifToggleCheck = new CheckBox();
            transPanel = new Panel();
            duoPicBox = new PictureBox();
            label5 = new Label();
            label2 = new Label();
            localeBox = new TextBox();
            displayPanel = new Panel();
            downloadProgressBar = new ProgressBar();
            discordCheck = new CheckBox();
            logRichBox = new RichTextBox();
            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pfpBox).BeginInit();
            megaPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)megaGifBox).BeginInit();
            transPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)duoPicBox).BeginInit();
            displayPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.BackColor = Color.WhiteSmoke;
            mainPanel.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Controls.Add(zipExtractCheck);
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
            // zipExtractCheck
            // 
            zipExtractCheck.AutoSize = true;
            zipExtractCheck.Checked = true;
            zipExtractCheck.CheckState = CheckState.Checked;
            zipExtractCheck.Location = new Point(84, 140);
            zipExtractCheck.Name = "zipExtractCheck";
            zipExtractCheck.Size = new Size(185, 19);
            zipExtractCheck.TabIndex = 21;
            zipExtractCheck.Text = "Automatically Extract ZIP Files";
            zipExtractCheck.UseVisualStyleBackColor = true;
            zipExtractCheck.CheckedChanged += zipExtractCheck_CheckedChanged;
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
            postNumBox.TextChanged += postNumBox_TextChanged;
            postNumBox.KeyDown += numberBox_EnterPressed;
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
            // testApiButton
            // 
            testApiButton.Location = new Point(3, 6);
            testApiButton.Name = "testApiButton";
            testApiButton.Size = new Size(128, 23);
            testApiButton.TabIndex = 20;
            testApiButton.Text = "Test Translation API";
            testApiButton.UseVisualStyleBackColor = true;
            testApiButton.Click += testTransButton_Click;
            // 
            // logLabel
            // 
            logLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            logLabel.Location = new Point(3, 221);
            logLabel.Name = "logLabel";
            logLabel.Size = new Size(766, 23);
            logLabel.TabIndex = 17;
            logLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // individualProgressBar
            // 
            individualProgressBar.Location = new Point(5, 276);
            individualProgressBar.Name = "individualProgressBar";
            individualProgressBar.Size = new Size(766, 23);
            individualProgressBar.Step = 1;
            individualProgressBar.TabIndex = 14;
            // 
            // nameLabel
            // 
            nameLabel.Location = new Point(307, 40);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(160, 15);
            nameLabel.TabIndex = 13;
            nameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pfpBox
            // 
            pfpBox.BorderStyle = BorderStyle.FixedSingle;
            pfpBox.Location = new Point(307, 58);
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
            postProcessBar.Step = 1;
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
            checkMegaSupport.Location = new Point(166, 400);
            checkMegaSupport.Name = "checkMegaSupport";
            checkMegaSupport.Size = new Size(146, 19);
            checkMegaSupport.TabIndex = 17;
            checkMegaSupport.Text = "Download MEGA Links";
            checkMegaSupport.UseVisualStyleBackColor = true;
            checkMegaSupport.CheckedChanged += checkMegaSupport_CheckedChanged;
            // 
            // megaPanel
            // 
            megaPanel.BackColor = Color.WhiteSmoke;
            megaPanel.BorderStyle = BorderStyle.FixedSingle;
            megaPanel.Controls.Add(killMegaButton);
            megaPanel.Controls.Add(linkLabel1);
            megaPanel.Controls.Add(label1);
            megaPanel.Controls.Add(megaGifBox);
            megaPanel.Controls.Add(checkMegaSupport);
            megaPanel.Controls.Add(passwordLabel);
            megaPanel.Controls.Add(button1);
            megaPanel.Controls.Add(passwordBox);
            megaPanel.Controls.Add(megaCmdBox);
            megaPanel.Location = new Point(791, 12);
            megaPanel.Name = "megaPanel";
            megaPanel.Size = new Size(480, 426);
            megaPanel.TabIndex = 6;
            megaPanel.Click += panel1_Click;
            // 
            // killMegaButton
            // 
            killMegaButton.Location = new Point(3, 32);
            killMegaButton.Name = "killMegaButton";
            killMegaButton.Size = new Size(115, 23);
            killMegaButton.TabIndex = 24;
            killMegaButton.Text = "Kill MEGA Threads";
            killMegaButton.UseVisualStyleBackColor = true;
            killMegaButton.Click += killMegaButton_Click;
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
            // megaGifBox
            // 
            megaGifBox.Anchor = AnchorStyles.Top;
            megaGifBox.BorderStyle = BorderStyle.FixedSingle;
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
            // gifToggleCheck
            // 
            gifToggleCheck.AutoSize = true;
            gifToggleCheck.Location = new Point(5, 6);
            gifToggleCheck.Name = "gifToggleCheck";
            gifToggleCheck.Size = new Size(89, 19);
            gifToggleCheck.TabIndex = 21;
            gifToggleCheck.Text = "Disable GIFs";
            gifToggleCheck.UseVisualStyleBackColor = true;
            gifToggleCheck.CheckedChanged += gifToggleCheck_CheckedChanged;
            // 
            // transPanel
            // 
            transPanel.BackColor = Color.WhiteSmoke;
            transPanel.BorderStyle = BorderStyle.FixedSingle;
            transPanel.Controls.Add(testApiButton);
            transPanel.Controls.Add(duoPicBox);
            transPanel.Controls.Add(label5);
            transPanel.Controls.Add(label2);
            transPanel.Controls.Add(localeBox);
            transPanel.Controls.Add(translateCheck);
            transPanel.Controls.Add(translateDescCheck);
            transPanel.Location = new Point(791, 444);
            transPanel.Name = "transPanel";
            transPanel.Size = new Size(480, 333);
            transPanel.TabIndex = 7;
            transPanel.Click += panel1_Click;
            // 
            // duoPicBox
            // 
            duoPicBox.BorderStyle = BorderStyle.FixedSingle;
            duoPicBox.Image = (Image)resources.GetObject("duoPicBox.Image");
            duoPicBox.Location = new Point(107, 68);
            duoPicBox.Name = "duoPicBox";
            duoPicBox.Size = new Size(264, 194);
            duoPicBox.TabIndex = 19;
            duoPicBox.TabStop = false;
            duoPicBox.Click += duoPicBox_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label5.Location = new Point(312, 313);
            label5.Name = "label5";
            label5.Size = new Size(163, 15);
            label5.TabIndex = 18;
            label5.Text = "Translate your troubles away!";
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
            // displayPanel
            // 
            displayPanel.BackColor = Color.WhiteSmoke;
            displayPanel.BackgroundImageLayout = ImageLayout.Zoom;
            displayPanel.BorderStyle = BorderStyle.FixedSingle;
            displayPanel.Controls.Add(downloadProgressBar);
            displayPanel.Controls.Add(discordCheck);
            displayPanel.Controls.Add(logLabel);
            displayPanel.Controls.Add(nameLabel);
            displayPanel.Controls.Add(postProcessBar);
            displayPanel.Controls.Add(gifToggleCheck);
            displayPanel.Controls.Add(individualProgressBar);
            displayPanel.Controls.Add(pfpBox);
            displayPanel.Location = new Point(12, 444);
            displayPanel.Name = "displayPanel";
            displayPanel.Size = new Size(776, 333);
            displayPanel.TabIndex = 8;
            displayPanel.Click += panel1_Click;
            // 
            // downloadProgressBar
            // 
            downloadProgressBar.Location = new Point(5, 247);
            downloadProgressBar.MarqueeAnimationSpeed = 50;
            downloadProgressBar.Maximum = 1000;
            downloadProgressBar.Name = "downloadProgressBar";
            downloadProgressBar.Size = new Size(766, 23);
            downloadProgressBar.Step = 1;
            downloadProgressBar.TabIndex = 23;
            // 
            // discordCheck
            // 
            discordCheck.AutoSize = true;
            discordCheck.Location = new Point(5, 31);
            discordCheck.Name = "discordCheck";
            discordCheck.Size = new Size(142, 19);
            discordCheck.TabIndex = 22;
            discordCheck.Text = "Discord Rich Presence";
            discordCheck.UseVisualStyleBackColor = true;
            discordCheck.CheckedChanged += discordCheck_CheckedChanged;
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
            Controls.Add(displayPanel);
            Controls.Add(transPanel);
            Controls.Add(megaPanel);
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
            megaPanel.ResumeLayout(false);
            megaPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)megaGifBox).EndInit();
            transPanel.ResumeLayout(false);
            transPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)duoPicBox).EndInit();
            displayPanel.ResumeLayout(false);
            displayPanel.PerformLayout();
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
        private Panel megaPanel;
        private Button button1;
        private TextBox megaCmdBox;
        private PictureBox megaGifBox;
        private CheckBox gifToggleCheck;
        private LinkLabel linkLabel1;
        private Label label1;
        private CheckBox doNumbers;
        private Label logLabel;
        private Panel transPanel;
        private Label label2;
        private TextBox localeBox;
        private Panel displayPanel;
        private RichTextBox logRichBox;
        private TextBox chunksBox;
        private Label label3;
        private Label label4;
        private TextBox parallelBox;
        private Button killMegaButton;
        private Label label5;
        private PictureBox duoPicBox;
        private Button testApiButton;
        private CheckBox discordCheck;
        private ProgressBar downloadProgressBar;
        private CheckBox zipExtractCheck;
    }
}