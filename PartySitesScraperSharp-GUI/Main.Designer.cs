namespace PartySitesScraperSharp_GUI
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
            urlBox = new TextBox();
            scrapeButton = new Button();
            outputDirButton = new Button();
            outputDirBox = new TextBox();
            mainPanel = new Panel();
            urlLabel = new Label();
            descLabel = new Label();
            postProcessBar = new ProgressBar();
            postNumBox = new TextBox();
            translateCheck = new CheckBox();
            postSubfoldersCheck = new CheckBox();
            outputLabel = new Label();
            mainPanel.SuspendLayout();
            SuspendLayout();
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
            scrapeButton.Location = new Point(338, 371);
            scrapeButton.Name = "scrapeButton";
            scrapeButton.Size = new Size(95, 23);
            scrapeButton.TabIndex = 1;
            scrapeButton.Text = "Scrape Posts";
            scrapeButton.UseVisualStyleBackColor = true;
            scrapeButton.Click += scrapeButton_Click;
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
            // outputDirBox
            // 
            outputDirBox.Enabled = false;
            outputDirBox.Location = new Point(84, 3);
            outputDirBox.Name = "outputDirBox";
            outputDirBox.PlaceholderText = "Output Directory";
            outputDirBox.Size = new Size(689, 23);
            outputDirBox.TabIndex = 3;
            outputDirBox.TextAlign = HorizontalAlignment.Center;
            outputDirBox.TextChanged += outputDirBox_TextChanged;
            // 
            // mainPanel
            // 
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
            mainPanel.TabIndex = 4;
            mainPanel.Click += panel1_Click;
            mainPanel.Paint += mainPanel_Paint;
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
            translateCheck.Location = new Point(84, 90);
            translateCheck.Name = "translateCheck";
            translateCheck.Size = new Size(96, 19);
            translateCheck.TabIndex = 5;
            translateCheck.Text = "Translate Text";
            translateCheck.UseVisualStyleBackColor = true;
            translateCheck.CheckedChanged += checkBox1_CheckedChanged_1;
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
            postSubfoldersCheck.CheckedChanged += checkBox1_CheckedChanged;
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
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(mainPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Coomer Scraper";
            Load += Main_Load;
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox urlBox;
        private Button scrapeButton;
        private Button outputDirButton;
        private TextBox outputDirBox;
        private Panel mainPanel;
        private CheckBox postSubfoldersCheck;
        private CheckBox translateCheck;
        private TextBox postNumBox;
        private ProgressBar postProcessBar;
        private Label descLabel;
        private Label urlLabel;
        private Label outputLabel;
    }
}