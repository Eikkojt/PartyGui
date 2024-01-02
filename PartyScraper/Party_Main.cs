using DiscordRPC;
using Downloader;
using KemonoScraperSharp_GUI.Configs;
using KemonoScraperSharp_GUI.Helpers;
using Newtonsoft.Json;
using PartyLib.Bases;
using PartyLib.Config;
using PartyLib.Helpers;
using PartyLib.Mega;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace KemonoScraperSharp_GUI;

public partial class Party_Main : Form
{
    /// <summary>
    /// Form version (independent of partylib)
    /// </summary>
    public const string Version = "2.0.3";

    /// <summary>
    /// User GUI settings
    /// </summary>
    public static UserPreferences Preferences { get; set; } = new UserPreferences();

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
    /// Whether the GUI is in its initial loading phase
    /// </summary>
    public static bool InitialLoading { get; set; } = false;

    /// <summary>
    /// Discord RPC client
    /// </summary>
    public DiscordRpcClient? DiscordClient;

    /// <summary>
    /// Discord RichPresence
    /// </summary>
    private readonly RichPresence _presencePreset = new()
    {
        Details = "PartyLib " + PartyConfig.Version,
        State = "Idling...",
        Assets = new Assets()
        {
            LargeImageKey = "coomer-kemono",
            LargeImageText = "v" + Version,
        },
        Timestamps = new Timestamps()
        {
            Start = DateTime.Now.ToUniversalTime()
        },

        // Worlds most scuffed button declaration
        Buttons = new List<DiscordRPC.Button>
        {
            new DiscordRPC.Button()
            {
                Label = "Source Code",
                Url = "https://github.com/AmnesiaIsHere/PartyLib"
            }
        }.ToArray()
    };

    /// <summary>
    /// Class Constructor
    /// </summary>
    public Party_Main()
    {
        InitializeComponent();
    }

    #region Events

    /// <summary>
    /// Form loading event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Kemono_Main_Load(object sender, EventArgs e)
    {
        InitialLoading = true;

        #region FormsHelper Vars

        FormsHelper.MainForm = this;
        FormsHelper.DownloadProgressBar = this.downloadProgressBar;
        FormsHelper.AttachmentsProgressBar = this.individualProgressBar;
        FormsHelper.PostProgressBar = this.postProcessBar;

        #endregion FormsHelper Vars

        this.Text = this.Text + " " + Version;
        ActiveControl = null;

        #region Configs

        if (File.Exists("./megaconf.json"))
        {
            LogToOutput("Reading MEGA config file and populating values");
            string JSON = File.ReadAllText("./megaconf.json");
            MegaConfig? conf = JsonConvert.DeserializeObject<MegaConfig>(JSON);
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

        if (File.Exists("./prefs.json"))
        {
            LogToOutput("Reading user preferences file and populating values");
            string JSON = File.ReadAllText("./prefs.json");
            UserPreferences? config = JsonConvert.DeserializeObject<UserPreferences>(JSON);
            Preferences = config;
            discordCheck.Checked = config.DiscordRich;
        }

        #endregion Configs

        LogToOutput("PartyLib " + PartyConfig.Version + " loaded.");

        if (Preferences.DiscordRich)
        {
            DiscordClient = new DiscordRpcClient(DiscordHelper.ClientID);

            DiscordClient.Logger = DiscordHelper.ClientLogger;

            DiscordClient.Initialize();

            DiscordClient.SetPresence(_presencePreset);
        }
        InitialLoading = false;
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
    }

    private void scrapeButton_Click(object sender, EventArgs e)
    {
        LogToOutput("Starting new scraping job!");
        var watch = System.Diagnostics.Stopwatch.StartNew();
        DateTime startTime = DateTime.Now.ToUniversalTime();

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
        funcs.DownloadComplete += DownloadComplete;
        funcs.DownloadSuccess += DownloadSuccess;
        funcs.DownloadFailure += DownloadFailiure;
        funcs.DownloadProgressed += DownloadProgressed;
        funcs.ZipFileExtracted += ZipFileExtracted;
        funcs.ZipFileUnsuccessful += ZipFileFailed;
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

        if (Preferences.DiscordRich && DiscordClient != null)
        {
            RichPresence startPresence = _presencePreset.Clone();
            startPresence.State = "Starting scrape job...";
            startPresence.Details = startPresence.Details + "\nCreator " + creator.Name;
            startPresence.Assets.SmallImageKey = creator.GetProfilePictureURL();
            startPresence.Timestamps = new Timestamps()
            {
                Start = startTime
            };
            DiscordClient.SetPresence(startPresence);
        }

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

        postProcessBar.Value = 0;
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

        postProcessBar.Minimum = 0;
        postProcessBar.Step = 1;
        individualProgressBar.Minimum = 0;
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
                UpdateCrossThreadPresence("Scraping Singular Page...", creator.GetProfilePictureURL(), creator.Name, startTime);
                Posts = Posts.Concat(funcs.ScrapePage(0, posts)).ToList();
                Invoke(LogToOutput, "Scraped " + posts + " posts");
            }
            else
            {
                // Used for everything else
                for (var i = 0; i < pages; i++)
                {
                    Invoke(LogToLabel, "Scraping Page #" + (i + 1) + "/" + pages + "...");
                    UpdateCrossThreadPresence($"Scraping Page {i + 1}/{pages}...", creator.GetProfilePictureURL(), creator.Name, startTime);
                    Invoke(LogToOutput, $"Page #{i + 1} out of {pages} parsing");
                    Posts = Posts.Concat(funcs.ScrapePage(i, 50)).ToList();
                }
            }

            // Partial page scraper, used for scraping the final page from the series
            if (posts > 0 && !singlePage)
            {
                Invoke(LogToLabel, "Scraping final page...");
                UpdateCrossThreadPresence("Scraping Straggling Posts...", creator.GetProfilePictureURL(), creator.Name, startTime);
                Invoke(LogToOutput, $"Parsing last page with {posts} posts");
                Posts = Posts.Concat(funcs.ScrapePage(pages, posts)).ToList();
            }

            Invoke(LogToLabel, "Downloading posts...");
            // Begin parsing and downloading the posts
            for (var i = 0; i < Posts.Count; i++)
            {
                Invoke(LogToLabel, "Parsing Post #" + (i + 1) + "/" + Posts.Count + "...");
                UpdateCrossThreadPresence($"Scraping Post {i + 1}/{Posts.Count}...", creator.GetProfilePictureURL(), creator.Name, startTime);

                // Fetch the post
                var scrapedPost = Posts[i];
                Invoke(LogToOutput, $"Post \"{scrapedPost.Title}\" is being processed with {scrapedPost.Files.Count + scrapedPost.Attachments.Count} attachments");

                // Sanitize post title
                string sanitizedPostTitle = StringHelper.SanitizeText(scrapedPost.Title);

                // Do math for total number of attachments
                int totalAttachmentsCount = scrapedPost.Files.Count + scrapedPost.Attachments.Count;

                // Progress bar update
                Invoke(SetMaxProgressCount, new object[] { totalAttachmentsCount });

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
                    Invoke(LogToOutput, $"File subsection parsing module activated ({scrapedPost.Files.Count} files found)");
                    foreach (var file in scrapedPost.Files)
                    {
                        Invoke(LogToOutput, $"Beginning download of file \"{file.FileName}\"...");
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
                        Invoke(DoIndividualStep);
                    }
                }

                // Post attachments
                if (scrapedPost.Attachments != null)
                {
                    Invoke(LogToOutput, $"Attachment subsection parsing module activated ({scrapedPost.Attachments.Count} attachments found)");
                    foreach (var attachment in scrapedPost.Attachments)
                    {
                        Invoke(LogToOutput, $"Beginning download of attachment \"{attachment.FileName}\"...");
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
                        Invoke(DoIndividualStep);
                    }
                }

                // Step progress bar
                Invoke(DoProgressBarStep);
                Invoke(ResetIndividualBar);
            }

            StopScraping();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Invoke(LogToOutput, "Scraping completed in " + (elapsedMs / 1000) + " seconds");
            if (Preferences.DiscordRich && DiscordClient != null)
            {
                Invoke(DiscordClient.SetPresence, _presencePreset);
            }
        }).Start();
    }

    private void panel1_Click(object sender, EventArgs e)
    {
        ActiveControl = null;
    }

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
        string prefsjson = JsonConvert.SerializeObject(Preferences, Formatting.Indented);
        File.WriteAllText("./prefs.json", prefsjson);

        if (DiscordClient != null)
        {
            DiscordClient.Dispose();
        }
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

    private void richTextBox1_TextChanged(object sender, EventArgs e)
    {
        logRichBox.SelectionStart = logRichBox.Text.Length;
        logRichBox.ScrollToCaret();
    }

    private void chunksBox_TextChanged(object sender, EventArgs e)
    {
        if (chunksBox.Text.Length == 0) return;

        try
        {
            PartyConfig.DownloadFileParts = Int32.Parse(chunksBox.Text);
        }
        catch (Exception)
        {
            chunksBox.Text = "";
        }
    }

    private void parallelBox_TextChanged(object sender, EventArgs e)
    {
        if (parallelBox.Text.Length == 0) return;

        try
        {
            PartyConfig.ParallelDownloadParts = Int32.Parse(parallelBox.Text);
        }
        catch (Exception)
        {
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

    private void postNumBox_TextChanged(object sender, EventArgs e)
    {
        if (postNumBox.Text == "") return;

        try
        {
            Int32.Parse(postNumBox.Text);
        }
        catch (FormatException)
        {
            postNumBox.Text = "";
        }
    }

    private void discordCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (!InitialLoading)
        {
            Preferences.DiscordRich = discordCheck.Checked;
            DialogResult doRestart = MessageBox.Show("Please restart the program for this option to take effect. Restart now?", "Restart Required",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (doRestart == DialogResult.Yes)
            {
                Application.Restart();
                Environment.Exit(0);
            }
        }
    }

    #endregion Events

    #region Functions

    private void UpdateCrossThreadPresence(string newstate, string creatorpfp, string creatorname, DateTime startOfScrape)
    {
        if (Preferences.DiscordRich && DiscordClient != null)
        {
            RichPresence startPresence = _presencePreset.Clone();
            startPresence.State = newstate;
            startPresence.Assets.SmallImageKey = creatorpfp;
            startPresence.Assets.SmallImageText = creatorname;
            startPresence.Timestamps = new Timestamps()
            {
                Start = startOfScrape
            };
            DiscordClient.SetPresence(startPresence);
        }
    }

    private void StopScraping()
    {
        Invoke(LogToLabel, "Finishing up...");
        Invoke(LogToOutput, "Cleaning up settings...");

        // Re-enable GUI controls
        Invoke(EnableBoxes);
        Invoke(ClearImageBox);
        Invoke(ResetMainBar);

        Invoke(LogToLabel, "Done!");
    }

    private void DownloadComplete(object sender, AsyncCompletedEventArgs e, string fileName)
    {
        this.Invoke(FormsHelper.SetDownloadBar, new object[] { 0 });
    }

    private void DownloadSuccess(object sender, AsyncCompletedEventArgs e, string fileName)
    {
        this.Invoke(LogToOutput, new object[] { $"Attachment \"{fileName}\" has successfully downloaded!" });
    }

    private void DownloadFailiure(object sender, AsyncCompletedEventArgs e, string fileName, Exception error)
    {
        this.Invoke(LogToOutput, new object[] { $"Attachment \"{fileName}\" failed to download with exception \"{error.Message}\"!" });
    }

    private void DownloadProgressed(object sender, DownloadProgressChangedEventArgs e, string fileName)
    {
        this.Invoke(FormsHelper.SetDownloadBar, new object[] { (int)(Math.Round(e.ProgressPercentage, 2) * 10) });
    }

    private void ZipFileExtracted(object sender, string zipFile)
    {
        if (File.Exists(zipFile))
        {
            try
            {
                File.Delete(zipFile);
            }
            catch (Exception ex)
            {
                this.Invoke(LogToOutput, new object[] { "Failed to automatically delete zip file! Do I have access rights? Exception: " + ex.Message });
            }
        }
        this.Invoke(LogToOutput, new object[] { "ZIP file located at \"" + zipFile + "\" has been extracted and deleted automatically" });
    }

    private void ZipFileFailed(object sender, string zipFile, Exception e)
    {
        if (e.Message.Contains("already exists"))
        {
            if (File.Exists(zipFile))
            {
                try
                {
                    File.Delete(zipFile);
                    this.Invoke(LogToOutput, new object[] { "ZIP file located at \"" + zipFile + "\" failed to unzip because it overlaps files. Zip file has been deleted and previous copy was preserved." });
                }
                catch (Exception ex)
                {
                    this.Invoke(LogToOutput, new object[] { "Failed to automatically delete zip file! Do I have access rights? Exception: " + ex.Message });
                }
            }
        }
        else
        {
            if (File.Exists(zipFile))
            {
                try
                {
                    File.Delete(zipFile);
                }
                catch (Exception ex)
                {
                    this.Invoke(LogToOutput, new object[] { "Failed to automatically delete zip file! Do I have access rights? Exception: " + ex.Message });
                }
            }
            this.Invoke(LogToOutput, new object[] { "ZIP file located at \"" + zipFile + "\" has failed to unzip automatically. It has been deleted to avoid issues. Exception: " + e.Message });
        }
    }

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
}