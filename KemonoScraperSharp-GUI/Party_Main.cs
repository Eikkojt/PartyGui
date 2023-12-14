using System.Diagnostics;
using System.Net.Mail;
using System.Reflection;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using PartyLib;
using PartyLib.Bases;
using PartyLib.Config;
using PartyLib.Helpers;
using PartyLib.Mega;

namespace KemonoScraperSharp_GUI;

public partial class Party_Main : Form
{
    /// <summary>
    /// Form version (independent of partylib)
    /// </summary>
    public const string Version = "2.0.0-alpha";

    /// <summary>
    /// Path to dump the downloaded data to
    /// </summary>
    public static string? SavePath { get; set; } = "";

    /// <summary>
    /// Whether to create a subfolder for every post
    /// </summary>
    public static bool PostSubfolders { get; set; } = true;

    /// <summary>
    /// Whether to use post numbers to name post subfolders, if enabled
    /// </summary>
    public static bool DoPostNumbers { get; set; } = false;

    /// <summary>
    /// Whether to flush the post's descriptions to disk
    /// </summary>
    public static bool WriteDescriptions { get; set; } = true;

    /// <summary>
    /// Class Constructor
    /// </summary>
    public Party_Main()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Form loading event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Kemono_Main_Load(object sender, EventArgs e)
    {
        this.Text = this.Text + " " + Version;
        ActiveControl = null;
        if (File.Exists("./megaconf.json"))
        {
            LogToOutput("Reading MEGA config file and populating values");
            string JSON = File.ReadAllText("./megaconf.json");
            MegaConfig conf = JsonConvert.DeserializeObject<MegaConfig>(JSON);
            PartyConfig.MegaOptions = conf;
            this.checkMegaSupport.Checked = conf.EnableMegaSupport;
            this.megaCmdBox.Text = conf.MegaCMDPath;
        }

        if (File.Exists("./transconf.json"))
        {
            LogToOutput("Reading translation config file and populating values");
            string JSON = File.ReadAllText("./transconf.json");
            TranslationConfig? config = JsonConvert.DeserializeObject<TranslationConfig>(JSON);
            PartyConfig.TranslationConfig.TranslationLocaleCode = config.TranslationLocaleCode;
            localeBox.Text = config.TranslationLocaleCode;
        }
        LogToOutput("PartyLib " + PartyConfig.Version + " loaded.");
    }

    private void outputDirButton_Click(object sender, EventArgs e)
    {
        var folderPicker = new FolderBrowserDialog();
        var result = folderPicker.ShowDialog();
        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderPicker.SelectedPath))
            outputDirBox.Text = folderPicker.SelectedPath;
    }

    private void postSubfoldersCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (postSubfoldersCheck.Checked)
        {
            doNumbers.Enabled = true;
        }
        else
        {
            doNumbers.Checked = false;
            doNumbers.Enabled = false;
        }
    }

    private void urlBox_EnterPressed(object sender, KeyEventArgs e)
    {
        if (urlBox.Text == "") return;
        if (e.KeyCode == Keys.Enter) ActiveControl = null;
    }

    private void urlBox_FocusLost(object sender, EventArgs e)
    {
        if (urlBox.Text == "") return;
        ActiveControl = null;
    }

    private void numberBox_EnterPressed(object sender, KeyEventArgs e)
    {
        if (postNumBox.Text == "") return;

        if (e.KeyCode == Keys.Enter)
            try
            {
                ActiveControl = null;
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid number of posts!", "Error");
                postNumBox.Text = "";
            }
    }

    private void numberBox_FocusLost(object sender, EventArgs e)
    {
        if (postNumBox.Text == "") return;
        try
        {
            ActiveControl = null;
        }
        catch (FormatException)
        {
            MessageBox.Show("Invalid number of posts!", "Error");
            postNumBox.Text = "";
        }
    }

    private void scrapeButton_Click(object sender, EventArgs e)
    {
        LogToOutput("Starting new scraping job!");
        var watch = System.Diagnostics.Stopwatch.StartNew();

        #region Checks

        if (outputDirBox.Text == "")
        {
            LogToOutput("ERROR: empty output directory");
            MessageBox.Show("Please select an output directory!", "Error");
            return;
        }

        if (urlBox.Text == "")
        {
            LogToOutput("ERROR: empty creator url");
            MessageBox.Show("Please specify a creator URL!", "Error");
            return;
        }

        if (postNumBox.Text == "")
        {
            LogToOutput("ERRORL empty number of posts");
            MessageBox.Show("Please specify the number of posts!", "Error");
            return;
        }

        #endregion Checks

        #region Initialize PartyLib Classes

        LogToLabel("Initializing PartyLib...");
        var numberOfPostsFromBox = int.Parse(postNumBox.Text);
        var creator = new Creator(urlBox.Text);
        var funcs = new ScraperHelper(creator, numberOfPostsFromBox);
        LogToOutput($"Creator \"{creator.Name}\" parsed and scraper classes initialized!");

        #endregion Initialize PartyLib Classes

        #region Set Variables From Textboxes

        LogToLabel("Setting variables...");
        LogToLabel("Populating PartyLib config values with user input");
        SavePath = outputDirBox.Text;
        DoPostNumbers = doNumbers.Checked;
        PostSubfolders = postSubfoldersCheck.Checked;
        WriteDescriptions = writeDescCheck.Checked;
        PartyConfig.TranslationConfig.TranslateTitles = translateCheck.Checked;
        PartyConfig.TranslationConfig.TranslateDescriptions = translateDescCheck.Checked;

        #endregion Set Variables From Textboxes

        DisableBoxes(); // Prevent user input

        #region Translate Creator Name

        if (PartyConfig.TranslationConfig.TranslateTitles)
        {
            LogToOutput("Translation enabled, beginning translation service");
            try
            {
                LogToLabel("Translating creator name...");
                var creatorNameTrans = PartyConfig.TranslationConfig.Translator.TranslateAsync(creator.Name, "en").Result;
                creator.Name = creatorNameTrans.Translation;
            }
            catch (Exception ex)
            {
                LogToOutput(ex.ToString());
                MessageBox.Show("Translation API Ratelimit reached. Disabling translation for future jobs.", "Error");
                PartyConfig.TranslationConfig.TranslateTitles = false;
            }
        }

        #endregion Translate Creator Name

        LogToLabel("Fetching profile picture...");
        pfpBox.Image = creator.GetProfilePicture();
        nameLabel.Text = creator.Name;

        #region Create Folders

        LogToLabel("Creating directories...");
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
            LogToOutput($"Save path \"{SavePath}\" created");
        }

        if (!Directory.Exists(SavePath + "/" + creator.Name))
        {
            Directory.CreateDirectory(SavePath + "/" + creator.Name);
            LogToOutput($"Save path \"{SavePath + "/" + creator.Name}\" created");
        }

        #endregion Create Folders

        #region Progress Bar Initial Math

        postProcessBar.Value = 1;
        if (funcs.TotalRequestedPosts == 0)
        {
            var posts = MathHelper.DoPageMath(creator, funcs.TotalRequestedPosts);
            postProcessBar.Maximum = (posts.Pages * 50) + posts.LeftoverPosts;
        }
        else
        {
            postProcessBar.Maximum = funcs.TotalRequestedPosts;
        }
        LogToOutput("Progress bar maximum set to " + postProcessBar.Maximum);

        #endregion Progress Bar Initial Math

        postProcessBar.Minimum = 1;
        postProcessBar.Step = 1;
        individualProgressBar.Minimum = 1;
        individualProgressBar.Step = 1;

        LogToLabel("Spawning new thread...");
        LogToOutput("Creating PartyScraper Background Process thread, cross-thread communication supported");
        // Thank god for multi-threading!
        new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.CurrentThread.Name = "PartyScraper Background Process";

            Invoke(LogToLabel, "Initializing cache...");
            var Posts = new List<Post>();

            var pagesAndPosts = MathHelper.DoPageMath(creator, funcs.TotalRequestedPosts);
            var pages = pagesAndPosts.Pages;
            var posts = pagesAndPosts.LeftoverPosts;
            var singlePage = pagesAndPosts.IsSinglePage;

            // Full Page Scraper
            if (singlePage)
            {
                // Used if only grabbing the first page
                Invoke(LogToLabel, "Scraping single page...");
                Posts = Posts.Concat(funcs.ScrapePage(0, posts)).ToList();
                Invoke(LogToOutput, "Scraped " + posts + " posts");
            }
            else
            {
                // Used for everything else
                for (var i = 0; i < pages; i++)
                {
                    Invoke(LogToLabel, "Scraping Page #" + (i + 1) + "/" + pages + "...");
                    Invoke(LogToOutput, $"Page #{i + 1} out of {pages} parsing");
                    Posts = Posts.Concat(funcs.ScrapePage(i, 50)).ToList();
                }
            }

            // Partial page scraper, used for scraping the final page from the series
            if (posts > 0 && !singlePage)
            {
                Invoke(LogToLabel, "Scraping final page...");
                Invoke(LogToOutput, $"Parsing last page with {posts} posts");
                Posts = Posts.Concat(funcs.ScrapePage(pages, posts)).ToList();
            }

            Invoke(LogToLabel, "Downloading posts...");
            // Begin parsing and downloading the posts
            for (var i = 0; i < Posts.Count; i++)
            {
                Invoke(LogToLabel, "Parsing Post #" + (i + 1) + "/" + Posts.Count + "...");
                // Fetch the post
                var scrapedPost = Posts[i];
                Invoke(LogToOutput, $"Post \"{scrapedPost.Title}\" is being processed with {scrapedPost.Files.Count + scrapedPost.Attachments.Count} attachments");

                // Sanitize post title
                string sanitizedPostTitle = StringHelper.SanitizeText(scrapedPost.Title);

                // Do math for total number of attachments
                int totalAttachmentsCount = scrapedPost.Files.Count + scrapedPost.Attachments.Count;

                // Progress bar update
                Invoke(SetMaxProgressCount, new object[] { totalAttachmentsCount + 1 });

                // Make post subfolder
                if (totalAttachmentsCount == 0 && WriteDescriptions == false)
                {
                    Console.WriteLine("No data to write for post! Skipping...");
                }
                else
                {
                    if (PostSubfolders)
                    {
                        Invoke(LogToOutput, "Subfolders module enabled");
                        if (DoPostNumbers)
                        {
                            if (!Directory.Exists(SavePath + "/" + creator.Name + "/" + sanitizedPostTitle + " (Post #" + scrapedPost.ReverseIteration + ")"))
                            {
                                Directory.CreateDirectory(SavePath + "/" + creator.Name + "/" + sanitizedPostTitle + " (Post #" + scrapedPost.ReverseIteration + ")");
                            }
                        }
                        else
                        {
                            if (!Directory.Exists(SavePath + "/" + creator.Name + "/" + sanitizedPostTitle))
                            {
                                Directory.CreateDirectory(SavePath + "/" + creator.Name + "/" + sanitizedPostTitle);
                            }
                        }
                    }
                }

                // Post descriptions
                if (WriteDescriptions)
                {
                    Invoke(LogToOutput, "Post descriptions module enabled");
                    if (scrapedPost.Description != string.Empty)
                    {
                        var postIdFinder = new Regex("/post/(.*)");
                        var postIdMatch = postIdFinder.Match(Posts[i].URL);
                        var postId = postIdMatch.Groups[1].Value;
                        if (PostSubfolders)
                        {
                            if (DoPostNumbers)
                            {
                                File.WriteAllText(SavePath + "/" + creator.Name + "/" + sanitizedPostTitle + " (Post #" + scrapedPost.ReverseIteration + ")" + "/" + postId + ".txt", scrapedPost.Description);
                            }
                            else
                            {
                                File.WriteAllText(SavePath + "/" + creator.Name + "/" + sanitizedPostTitle + "/" + postId + ".txt", scrapedPost.Description);
                            }
                        }
                        else
                        {
                            File.WriteAllText(SavePath + "/" + creator.Name + "/" + postId + ".txt", scrapedPost.Description);
                        }
                    }
                }

                // Mega files
                if (scrapedPost.MegaUrls.Count > 0 && PartyConfig.MegaOptions.EnableMegaSupport)
                {
                    Invoke(LogToOutput, "Experimental MEGA module enabled");
                    MegaDownloader downloader = new MegaDownloader();
                    foreach (string url in scrapedPost.MegaUrls)
                    {
                        if (PostSubfolders)
                        {
                            if (DoPostNumbers)
                            {
                                downloader.ExecuteMegaGet(url,
                                    SavePath + "/" + creator.Name + "/" + sanitizedPostTitle + " (Post #" +
                                    scrapedPost.ReverseIteration + ")", Invoke(GetPasswordText));
                            }
                            else
                            {
                                downloader.ExecuteMegaGet(url,
                                    SavePath + "/" + creator.Name + "/" + sanitizedPostTitle, Invoke(GetPasswordText));
                            }
                        }
                        else
                        {
                            downloader.ExecuteMegaGet(url, SavePath + "/" + creator.Name, Invoke(GetPasswordText));
                        }
                    }
                }

                // Post files
                if (scrapedPost.Files != null)
                {
                    Invoke(LogToOutput, "File subsection parsing module activated");
                    foreach (var file in scrapedPost.Files)
                    {
                        bool success;
                        if (PostSubfolders)
                        {
                            if (DoPostNumbers)
                            {
                                success = funcs.DownloadAttachment(file, SavePath + "/" + creator.Name + "/" + sanitizedPostTitle + " (Post #" + scrapedPost.ReverseIteration + ")");
                            }
                            else
                            {
                                success = funcs.DownloadAttachment(file,
                                    SavePath + "/" + creator.Name + "/" + sanitizedPostTitle);
                            }
                        }
                        else
                        {
                            success = funcs.DownloadAttachment(file, SavePath + "/" + creator.Name);
                        }
                        if (!success)
                        {
                            Invoke(LogToOutput,
                                $"ERROR: Attachment \"{file.FileName}\" failed to download for post \"{scrapedPost.Title}\" with ID {scrapedPost.ID}. File URL is \"{file.URL}\". Download has been skipped");
                            if (PartyConfig.DownloaderErrors.Exists(x => x.Item2 == file.FileName))
                            {
                                Exception downloadException = PartyConfig.DownloaderErrors.FirstOrDefault(x => x.Item2 == file.FileName).Item1;
                                Invoke(LogToOutput, "Exception found from download: " + downloadException.Message);
                            }
                        }
                        else
                        {
                            Invoke(LogToOutput, $"Successfully downloaded file {file.FileName}!");
                        }
                        Invoke(DoIndividualStep);
                    }
                }

                // Post attachments
                if (scrapedPost.Attachments != null)
                {
                    Invoke(LogToOutput, "Attachment subsection parsing module activated");
                    foreach (var attachment in scrapedPost.Attachments)
                    {
                        bool success;
                        if (PostSubfolders)
                        {
                            if (DoPostNumbers)
                            {
                                success = funcs.DownloadAttachment(attachment,
                                    SavePath + "/" + creator.Name + "/" + sanitizedPostTitle + " (Post #" +
                                    scrapedPost.ReverseIteration + ")");
                            }
                            else
                            {
                                success = funcs.DownloadAttachment(attachment,
                                    SavePath + "/" + creator.Name + "/" + sanitizedPostTitle);
                            }
                        }
                        else
                        {
                            success = funcs.DownloadAttachment(attachment, SavePath + "/" + creator.Name);
                        }
                        if (!success)
                        {
                            Invoke(LogToOutput,
                                $"ERROR: Attachment \"{attachment.FileName}\" failed to download for post \"{scrapedPost.Title}\" with ID {scrapedPost.ID}. Attachment URL is \"{attachment.URL}\". Download has been skipped");
                            if (PartyConfig.DownloaderErrors.Exists(x => x.Item2 == attachment.FileName))
                            {
                                Exception downloadException = PartyConfig.DownloaderErrors.FirstOrDefault(x => x.Item2 == attachment.FileName).Item1;
                                Invoke(LogToOutput, "Exception found from download: " + downloadException.Message);
                            }
                        }
                        else
                        {
                            Invoke(LogToOutput, $"Successfully downloaded attachment {attachment.FileName}!");
                        }
                        Invoke(DoIndividualStep);
                    }
                }

                // Step progress bar
                Invoke(DoProgressBarStep);
                Invoke(ResetIndividualBar);
            }

            Invoke(LogToLabel, "Finishing up...");
            Invoke(LogToOutput, "Cleaning up settings...");

            // Re-enable GUI controls
            Invoke(EnableBoxes);
            Invoke(ClearImageBox);
            Invoke(ResetMainBar);
            PartyConfig.DownloaderErrors.Clear();

            Invoke(LogToLabel, "Done!");
            // Get execution time
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Invoke(LogToOutput, "Scraping completed in " + (elapsedMs / 1000) + " seconds");
            Invoke(ShowMessageBox,
                new object[] { "Scrape Complete", "Scraping completed in " + (elapsedMs / 1000) + "s!" });
        }).Start();
    }

    private void panel1_Click(object sender, EventArgs e)
    {
        ActiveControl = null;
    }

    #region Functions

    public void LogToLabel(string message)
    {
        this.logLabel.Text = message;
    }

    public void LogToOutput(string message)
    {
        if (this.logRichBox.Text == "")
        {
            this.logRichBox.Text = "[+] " + message;
        }
        else
        {
            this.logRichBox.Text = this.logRichBox.Text + "\n[+] " + message;
        }
    }

    private string GetPasswordText()
    {
        return this.passwordBox.Text;
    }

    private void DoProgressBarStep()
    {
        postProcessBar.PerformStep();
    }

    private void DoIndividualStep()
    {
        individualProgressBar.PerformStep();
    }

    private void SetMaxProgressCount(int num)
    {
        individualProgressBar.Maximum = num;
    }

    private void ResetIndividualBar()
    {
        individualProgressBar.Value = individualProgressBar.Minimum;
    }

    private void ResetMainBar()
    {
        postProcessBar.Value = postProcessBar.Minimum;
    }

    private void DisableBoxes()
    {
        LogToOutput("User input disabled");
        urlBox.Enabled = false;
        postNumBox.Enabled = false;
        scrapeButton.Enabled = false;
        outputDirButton.Enabled = false;
        passwordBox.Enabled = false;
        localeBox.Enabled = false;
        chunksBox.Enabled = false;
        parallelBox.Enabled = false;
    }

    private void EnableBoxes()
    {
        LogToOutput("User input enabled");
        urlBox.Enabled = true;
        postNumBox.Enabled = true;
        scrapeButton.Enabled = true;
        outputDirButton.Enabled = true;
        chunksBox.Enabled = true;
        parallelBox.Enabled = true;
        if (checkMegaSupport.Checked)
        {
            passwordBox.Enabled = true;
        }

        if (translateCheck.Checked || translateDescCheck.Checked)
        {
            localeBox.Enabled = true;
        }
        this.logLabel.Text = "";
    }

    private void ClearImageBox()
    {
        LogToOutput("Image box cleared");
        pfpBox.Image = null;
        nameLabel.Text = string.Empty;
    }

    private void ShowMessageBox(string Title, string Message)
    {
        MessageBox.Show(Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    #endregion Functions

    private void pfpBox_Click(object sender, EventArgs e)
    {
        ActiveControl = null;
    }

    private void writeDescCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (writeDescCheck.Checked)
        {
            translateDescCheck.Enabled = true;
        }
        else
        {
            translateDescCheck.Checked = false;
            translateDescCheck.Enabled = false;
        }
    }

    private void checkMegaSupport_CheckedChanged(object sender, EventArgs e)
    {
        PartyConfig.MegaOptions.EnableMegaSupport = checkMegaSupport.Checked;
        if (checkMegaSupport.Checked)
        {
            this.passwordBox.Enabled = true;
        }
        else
        {
            this.passwordBox.Enabled = false;
        }
    }

    private void Party_Main_FormClosing(object sender, FormClosingEventArgs e)
    {
        string json = JsonConvert.SerializeObject(PartyConfig.MegaOptions, Formatting.Indented);
        File.WriteAllText("./megaconf.json", json);
        string transjson = JsonConvert.SerializeObject(PartyConfig.TranslationConfig, Formatting.Indented);
        File.WriteAllText("./transconf.json", transjson);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        var folderPicker = new FolderBrowserDialog();
        var result = folderPicker.ShowDialog();
        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderPicker.SelectedPath))
            megaCmdBox.Text = folderPicker.SelectedPath;
    }

    private void megaCmdBox_TextChanged(object sender, EventArgs e)
    {
        PartyConfig.MegaOptions.MegaCMDPath = megaCmdBox.Text;
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
        ActiveControl = null;
    }

    private void gifToggleCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (gifToggleCheck.Checked)
        {
            this.megaGifBox.Visible = false;
            this.duoPicBox.Visible = false;
        }
        else
        {
            this.megaGifBox.Visible = true;
            this.duoPicBox.Visible = true;
        }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        System.Diagnostics.Process.Start(new ProcessStartInfo("https://www.proxifier.com/") { UseShellExecute = true });
    }

    private void translateCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (translateCheck.Checked || translateDescCheck.Checked)
        {
            localeBox.Enabled = true;
        }
        else
        {
            localeBox.Enabled = false;
        }
    }

    private void translateDescCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (translateCheck.Checked || translateDescCheck.Checked)
        {
            localeBox.Enabled = true;
        }
        else
        {
            localeBox.Enabled = false;
        }
    }

    private void localeBox_TextChanged(object sender, EventArgs e)
    {
        PartyConfig.TranslationConfig.TranslationLocaleCode = localeBox.Text;
    }

    private void panel2_Paint(object sender, PaintEventArgs e)
    {
        this.ActiveControl = null;
    }

    private void panel3_Paint(object sender, PaintEventArgs e)
    {
        this.ActiveControl = null;
    }

    private void richTextBox1_TextChanged(object sender, EventArgs e)
    {
        logRichBox.SelectionStart = logRichBox.Text.Length;
        logRichBox.ScrollToCaret();
    }

    private void chunksBox_TextChanged(object sender, EventArgs e)
    {
        try
        {
            PartyConfig.DownloadFileParts = Int32.Parse(chunksBox.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Not a valid number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            chunksBox.Text = "";
        }
    }

    private void parallelBox_TextChanged(object sender, EventArgs e)
    {
        try
        {
            PartyConfig.ParallelDownloadParts = Int32.Parse(parallelBox.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Not a valid number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            parallelBox.Text = "";
        }
    }

    private void killMegaButton_Click(object sender, EventArgs e)
    {
        Process[] megaServers = Process.GetProcessesByName("MEGAcmdServer");
        foreach (Process megaServer in megaServers)
        {
            megaServer.Kill();
        }
    }

    private void testTransButton_Click(object sender, EventArgs e)
    {
        try
        {
            PartyConfig.TranslationConfig.Translator.TranslateAsync("Hola", PartyConfig.TranslationConfig.TranslationLocaleCode).Wait();
            MessageBox.Show("Translation service operational! Built-in translator may be used without problems.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch
        {
            MessageBox.Show(
                "Google translation API errored! You are most likely being ratelimited. Please try again in 24 hours.",
                "Failiure", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void duoPicBox_Click(object sender, EventArgs e)
    {
        this.ActiveControl = null;
    }
}