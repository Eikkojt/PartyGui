using System.Diagnostics;
using System.Net.Mail;
using System.Reflection;
using System.Security.Policy;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using PartyLib;
using PartyLib.Bases;
using PartyLib.Config;
using PartyLib.Mega;

namespace KemonoScraperSharp_GUI;

public partial class Party_Main : Form
{
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

    public Party_Main()
    {
        InitializeComponent();
    }

    private void Kemono_Main_Load(object sender, EventArgs e)
    {
        this.Text = this.Text + " " + PartyConfig.Version;
        ActiveControl = null;
        if (File.Exists("./megaconf.json"))
        {
            string JSON = File.ReadAllText("./megaconf.json");
            MegaConfig conf = JsonConvert.DeserializeObject<MegaConfig>(JSON);
            PartyConfig.MegaOptions = conf;
            this.checkMegaSupport.Checked = conf.EnableMegaSupport;
            this.megaCmdBox.Text = conf.MegaCMDPath;
        }
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
        var watch = System.Diagnostics.Stopwatch.StartNew();

        #region Checks

        if (outputDirBox.Text == "")
        {
            MessageBox.Show("Please select an output directory!", "Error");
            return;
        }

        if (urlBox.Text == "")
        {
            MessageBox.Show("Please specify a creator URL!", "Error");
            return;
        }

        if (postNumBox.Text == "")
        {
            MessageBox.Show("Please specify the number of posts!", "Error");
            return;
        }

        #endregion Checks

        #region Initialize PartyLib Classes

        var numberOfPostsFromBox = int.Parse(postNumBox.Text);
        var creator = new Creator(urlBox.Text);
        var funcs = new ScraperFunctions(creator, numberOfPostsFromBox);

        #endregion Initialize PartyLib Classes

        #region Set Variables From Textboxes

        SavePath = outputDirBox.Text;
        DoPostNumbers = doNumbers.Checked;
        PostSubfolders = postSubfoldersCheck.Checked;
        WriteDescriptions = writeDescCheck.Checked;
        PartyConfig.TranslateTitles = translateCheck.Checked;
        PartyConfig.TranslateDescriptions = translateDescCheck.Checked;

        #endregion Set Variables From Textboxes

        DisableBoxes(); // Prevent user input

        #region Translate Creator Name

        if (PartyConfig.TranslateTitles)
        {
            try
            {
                var creatorNameTrans = PartyConfig.Translator.TranslateAsync(creator.Name, "en").Result;
                creator.Name = creatorNameTrans.Translation;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Translation API Ratelimit reached. Disabling translation for future jobs.", "Error");
                PartyConfig.TranslateTitles = false;
            }
        }

        #endregion Translate Creator Name

        pfpBox.Image = creator.GetProfilePicture();
        nameLabel.Text = creator.Name;

        #region Create Folders

        if (!Directory.Exists(SavePath)) Directory.CreateDirectory(SavePath);

        if (!Directory.Exists(SavePath + "/" + creator.Name))
            Directory.CreateDirectory(SavePath + "/" + creator.Name);

        #endregion Create Folders

        #region Progress Bar Initial Math

        postProcessBar.Value = 1;
        if (funcs.TotalRequestedPosts == 0)
        {
            var posts = funcs.DoPageMath();
            postProcessBar.Maximum = posts.Pages + posts.LeftoverPosts * 50;
        }
        else
        {
            postProcessBar.Maximum = funcs.TotalRequestedPosts;
        }

        #endregion Progress Bar Initial Math

        postProcessBar.Minimum = 1;
        postProcessBar.Step = 1;
        individualProgressBar.Minimum = 1;
        individualProgressBar.Step = 1;

        // Thank god for multi-threading!
        new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.CurrentThread.Name = "PartyScraper Background Process";

            var Posts = new List<Post>();

            var pagesAndPosts = funcs.DoPageMath();
            var pages = pagesAndPosts.Pages;
            var posts = pagesAndPosts.LeftoverPosts;
            var singlePage = pagesAndPosts.IsSinglePage;

            // Full Page Scraper
            if (singlePage)
            {
                // Used if only grabbing the first page
                Posts = Posts.Concat(funcs.ScrapePage(0, posts)).ToList();
            }
            else
            {
                // Used for everything else
                for (var i = 0; i < pages; i++)
                {
                    Posts = Posts.Concat(funcs.ScrapePage(i, 50)).ToList();
                }
            }

            // Partial page scraper, used for scraping the final page from the series
            if (posts > 0 && !singlePage)
            {
                Posts = Posts.Concat(funcs.ScrapePage(pages, posts)).ToList();
            }

            // Begin parsing and downloading the posts
            for (var i = 0; i < Posts.Count; i++)
            {
                // Fetch the post
                var scrapedPost = Posts[i];

                // Sanitize post title
                string sanitizedPostTitle = Strings.SanitizeText(scrapedPost.Title);

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
                            //MessageBox.Show("Image/File failed to download!", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                        Invoke(DoIndividualStep);
                    }
                }

                // Post attachments
                if (scrapedPost.Attachments != null)
                {
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
                            //MessageBox.Show("Attachment failed to download!", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                        Invoke(DoIndividualStep);
                    }
                }

                // Step progress bar
                Invoke(DoProgressBarStep);
                Invoke(ResetIndividualBar);
            }

            // Re-enable GUI controls
            Invoke(EnableBoxes);
            Invoke(ClearImageBox);
            Invoke(ResetMainBar);

            // Get execution time
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Invoke(ShowMessageBox,
                new object[] { "Scrape Complete", "Scraping completed in " + (elapsedMs / 1000) + "s!" });
        }).Start();
    }

    private void panel1_Click(object sender, EventArgs e)
    {
        ActiveControl = null;
    }

    #region Functions

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
        urlBox.Enabled = false;
        postNumBox.Enabled = false;
        scrapeButton.Enabled = false;
        outputDirButton.Enabled = false;
        passwordBox.Enabled = false;
    }

    private void EnableBoxes()
    {
        urlBox.Enabled = true;
        postNumBox.Enabled = true;
        scrapeButton.Enabled = true;
        outputDirButton.Enabled = true;
        if (checkMegaSupport.Checked)
        {
            passwordBox.Enabled = true;
        }
    }

    private void ClearImageBox()
    {
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
        }
        else
        {
            this.megaGifBox.Visible = true;
        }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        System.Diagnostics.Process.Start(new ProcessStartInfo("https://www.proxifier.com/") { UseShellExecute = true });
    }
}