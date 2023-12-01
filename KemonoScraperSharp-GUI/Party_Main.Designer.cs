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
            writeDescCheck = new CheckBox();
            translateDescCheck = new CheckBox();
            individualProgressBar = new ProgressBar();
            nameLabel = new Label();
            pfpBox = new PictureBox();
            doNumbers = new CheckBox();
            outputLabel = new Label();
            urlLabel = new Label();
            descLabel = new Label();
            postProcessBar = new ProgressBar();
            postNumBox = new TextBox();
            translateCheck = new CheckBox();
            postSubfoldersCheck = new CheckBox();
            urlBox = new TextBox();
            scrapeButton = new Button();
            outputDirBox = new TextBox();
            outputDirButton = new Button();
            passwordLabel = new Label();
            passwordBox = new TextBox();
            checkMegaSupport = new CheckBox();
            panel1 = new Panel();
            gifToggleCheck = new CheckBox();
            megaGifBox = new PictureBox();
            button1 = new Button();
            megaCmdBox = new TextBox();
            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pfpBox).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)megaGifBox).BeginInit();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.BackColor = Color.WhiteSmoke;
            mainPanel.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Controls.Add(writeDescCheck);
            mainPanel.Controls.Add(translateDescCheck);
            mainPanel.Controls.Add(individualProgressBar);
            mainPanel.Controls.Add(nameLabel);
            mainPanel.Controls.Add(pfpBox);
            mainPanel.Controls.Add(doNumbers);
            mainPanel.Controls.Add(outputLabel);
            mainPanel.Controls.Add(urlLabel);
            mainPanel.Controls.Add(descLabel);
            mainPanel.Controls.Add(postProcessBar);
            mainPanel.Controls.Add(postNumBox);
            mainPanel.Controls.Add(translateCheck);
            mainPanel.Controls.Add(postSubfoldersCheck);
            mainPanel.Controls.Add(urlBox);
            mainPanel.Controls.Add(scrapeButton);
            mainPanel.Controls.Add(outputDirBox);
            mainPanel.Controls.Add(outputDirButton);
            mainPanel.Location = new Point(12, 12);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(776, 426);
            mainPanel.TabIndex = 5;
            mainPanel.Click += panel1_Click;
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
            // translateDescCheck
            // 
            translateDescCheck.AutoSize = true;
            translateDescCheck.Enabled = false;
            translateDescCheck.Location = new Point(84, 140);
            translateDescCheck.Name = "translateDescCheck";
            translateDescCheck.Size = new Size(140, 19);
            translateDescCheck.TabIndex = 15;
            translateDescCheck.Text = "Translate Descriptions";
            translateDescCheck.UseVisualStyleBackColor = true;
            // 
            // individualProgressBar
            // 
            individualProgressBar.Location = new Point(3, 371);
            individualProgressBar.Name = "individualProgressBar";
            individualProgressBar.Size = new Size(770, 23);
            individualProgressBar.TabIndex = 14;
            // 
            // nameLabel
            // 
            nameLabel.Location = new Point(311, 157);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(160, 15);
            nameLabel.TabIndex = 13;
            nameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pfpBox
            // 
            pfpBox.Location = new Point(311, 175);
            pfpBox.Name = "pfpBox";
            pfpBox.Size = new Size(160, 160);
            pfpBox.TabIndex = 12;
            pfpBox.TabStop = false;
            pfpBox.Click += pfpBox_Click;
            // 
            // doNumbers
            // 
            doNumbers.AutoSize = true;
            doNumbers.Location = new Point(84, 165);
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
            // postProcessBar
            // 
            postProcessBar.Location = new Point(3, 400);
            postProcessBar.Name = "postProcessBar";
            postProcessBar.Size = new Size(770, 23);
            postProcessBar.TabIndex = 7;
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
            // translateCheck
            // 
            translateCheck.AutoSize = true;
            translateCheck.Location = new Point(84, 115);
            translateCheck.Name = "translateCheck";
            translateCheck.Size = new Size(128, 19);
            translateCheck.TabIndex = 5;
            translateCheck.Text = "Translate Post Titles";
            translateCheck.UseVisualStyleBackColor = true;
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
            // scrapeButton
            // 
            scrapeButton.Location = new Point(343, 341);
            scrapeButton.Name = "scrapeButton";
            scrapeButton.Size = new Size(95, 23);
            scrapeButton.TabIndex = 1;
            scrapeButton.Text = "Scrape Posts";
            scrapeButton.UseVisualStyleBackColor = true;
            scrapeButton.Click += scrapeButton_Click;
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
            // Party_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1286, 450);
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
        private CheckBox doNumbers;
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
    }
}