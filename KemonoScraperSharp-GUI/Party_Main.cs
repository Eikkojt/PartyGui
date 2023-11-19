using System.Reflection;
using System.Text.RegularExpressions;
using PartyLib;
using PartyLib.Bases;

namespace KemonoScraperSharp_GUI;

public partial class Party_Main : Form
{
    public Party_Main()
    {
        InitializeComponent();
    }

    private void Kemono_Main_Load(object sender, EventArgs e)
    {
        ActiveControl = null;
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

    private void translateCheck_CheckedChanged(object sender, EventArgs e)
    {
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

    private void outputDirBox_TextChanged(object sender, EventArgs e)
    {
    }

    private void scrapeButton_Click(object sender, EventArgs e)
    {
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

        PartyGlobals.SavePath = outputDirBox.Text;
        PartyGlobals.DoPostNumbers = doNumbers.Checked;
        PartyGlobals.PostSubfolders = postSubfoldersCheck.Checked;
        PartyGlobals.TranslateTitles = translateCheck.Checked;
        PartyGlobals.TranslateDescriptions = translateDescCheck.Checked;

        #endregion Set Variables From Textboxes

        DisableBoxes(); // Prevent user input

        #region Translate Creator Name

        if (PartyGlobals.TranslateTitles)
        {
            try
            {
                var creatorNameTrans = PartyGlobals.Translator.TranslateAsync(creator.Name, "en").Result;
                creator.Name = creatorNameTrans.Translation;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Translation API Ratelimit reached. Disabling translation for future jobs.", "Error");
                PartyGlobals.TranslateTitles = false;
            }
        }

        #endregion Translate Creator Name

        pfpBox.Image = creator.GetProfilePicture();
        nameLabel.Text = creator.Name;

        #region Create Folders

        if (!Directory.Exists(PartyGlobals.SavePath)) Directory.CreateDirectory(PartyGlobals.SavePath);

        if (!Directory.Exists(PartyGlobals.SavePath + "/" + creator.Name))
            Directory.CreateDirectory(PartyGlobals.SavePath + "/" + creator.Name);

        #endregion Create Folders

        #region Progress Bar Initial Math

        postProcessBar.Value = 1;
        if (funcs.TotalRequestedPosts == 0)
        {
            var posts = funcs.DoPageMath();
            postProcessBar.Maximum = posts.Item1 + posts.Item2 * 50;
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

            var postUrls = new List<string>();

            var pagesAndPosts = funcs.DoPageMath();
            var pages = pagesAndPosts.Item1;
            var posts = pagesAndPosts.Item2;
            var singlePage = pagesAndPosts.Item3;

            // Full Page Scraper
            if (singlePage)
            {
                // Used if only grabbing the first page
                postUrls = postUrls.Concat(funcs.ScrapePage(0, posts)).ToList();
            }
            else
            {
                // Used for everything else
                for (var i = 0; i < pages; i++)
                {
                    postUrls = postUrls.Concat(funcs.ScrapePage(i, 50)).ToList();
                }
            }

            // Partial page scraper, used for scraping the final page from the series
            if (posts > 0 && !singlePage)
            {
                postUrls = postUrls.Concat(funcs.ScrapePage(pages, posts)).ToList();
            }

            // Begin parsing and downloading the posts
            for (var i = 0; i < postUrls.Count; i++)
            {
                var scrapedPost = funcs.ScrapePost(postUrls[i], i + 1);
                int totalAttachmentsCount = scrapedPost.Images.Count + scrapedPost.Attachments.Count;
                Invoke(SetMaxProgressCount, new object[] { totalAttachmentsCount });

                // Make post subfolder
                if (PartyGlobals.PostSubfolders)
                {
                    if (PartyGlobals.DoPostNumbers)
                    {
                        if (!Directory.Exists(PartyGlobals.SavePath + "/" + creator.Name + "/" + scrapedPost.Title + " (Post #" + scrapedPost.ReverseIteration + ")"))
                        {
                            Directory.CreateDirectory(PartyGlobals.SavePath + "/" + creator.Name + "/" + scrapedPost.Title + " (Post #" + scrapedPost.ReverseIteration + ")");
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(PartyGlobals.SavePath + "/" + creator.Name + "/" + scrapedPost.Title))
                        {
                            Directory.CreateDirectory(PartyGlobals.SavePath + "/" + creator.Name + "/" + scrapedPost.Title);
                        }
                    }
                }

                // Post descriptions
                if (scrapedPost.Description != string.Empty)
                {
                    var postIdFinder = new Regex("/post/(.*)");
                    var postIdMatch = postIdFinder.Match(postUrls[i]);
                    var postId = postIdMatch.Groups[1].Value;
                    if (PartyGlobals.PostSubfolders)
                    {
                        if (PartyGlobals.DoPostNumbers)
                        {
                            File.WriteAllText(PartyGlobals.SavePath + "/" + creator.Name + "/" + scrapedPost.Title + " (Post #" + scrapedPost.ReverseIteration + ")" + "/" + postId + ".txt", scrapedPost.Description);
                        }
                        else
                        {
                            File.WriteAllText(PartyGlobals.SavePath + "/" + creator.Name + "/" + scrapedPost.Title + "/" + postId + ".txt", scrapedPost.Description);
                        }
                    }
                    else
                    {
                        File.WriteAllText(PartyGlobals.SavePath + "/" + creator.Name + "/" + postId + ".txt", scrapedPost.Description);
                    }
                }

                // Post images
                if (scrapedPost.Images != null)
                {
                    foreach (var image in scrapedPost.Images)
                    {
                        if (PartyGlobals.PostSubfolders)
                        {
                            if (PartyGlobals.DoPostNumbers)
                            {
                                funcs.DownloadAttachment(image,
                                    PartyGlobals.SavePath + "/" + creator.Name + "/" + scrapedPost.Title + " (Post #" +
                                    scrapedPost.ReverseIteration + ")");
                            }
                            else
                            {
                                funcs.DownloadAttachment(image,
                                    PartyGlobals.SavePath + "/" + creator.Name + "/" + scrapedPost.Title);
                            }
                        }
                        else
                        {
                            funcs.DownloadAttachment(image, PartyGlobals.SavePath + "/" + creator.Name);
                        }
                        Invoke(DoIndividualStep);
                    }
                }

                // Post attachments
                if (scrapedPost.Attachments != null)
                {
                    foreach (var attachment in scrapedPost.Attachments)
                    {
                        if (PartyGlobals.PostSubfolders)
                        {
                            if (PartyGlobals.DoPostNumbers)
                            {
                                funcs.DownloadAttachment(attachment,
                                    PartyGlobals.SavePath + "/" + creator.Name + "/" + scrapedPost.Title + " (Post #" +
                                    scrapedPost.ReverseIteration + ")");
                            }
                            else
                            {
                                funcs.DownloadAttachment(attachment,
                                    PartyGlobals.SavePath + "/" + creator.Name + "/" + scrapedPost.Title);
                            }
                        }
                        else
                        {
                            funcs.DownloadAttachment(attachment, PartyGlobals.SavePath + "/" + creator.Name);
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
        }).Start();
    }

    private void panel1_Click(object sender, EventArgs e)
    {
        ActiveControl = null;
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void mainPanel_Paint(object sender, PaintEventArgs e)
    {
    }

    #region Functions

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
    }

    private void EnableBoxes()
    {
        urlBox.Enabled = true;
        postNumBox.Enabled = true;
        scrapeButton.Enabled = true;
        outputDirButton.Enabled = true;
    }

    private void ClearImageBox()
    {
        pfpBox.Image = null;
        nameLabel.Text = string.Empty;
    }

    #endregion Functions

    private void progressBar1_Click(object sender, EventArgs e)
    {
    }

    private void translateDescCheck_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void pfpBox_Click(object sender, EventArgs e)
    {
    }
}