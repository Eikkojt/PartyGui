using RestSharp;
using RestSharp.Extensions;
using HtmlAgilityPack;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System;
using System.IO;
using System.ComponentModel.Design;
using System.Diagnostics;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Drawing.Text;
using GTranslate.Translators;
using OctaneEngine;
using OctaneEngineCore;

namespace PartySitesScraperSharp_GUI
{
    public partial class Main : Form
    {
        #region Variables

        private string? creatorUrl = "";
        private string? savePath = "";
        private int numberOfPosts = -1;
        private int numberOfPosts_static = 0;
        private List<string> postUrls = new List<string>();
        private GoogleTranslator translator = new GoogleTranslator();

        // Options
        private bool postSubfolders = true;

        private bool translateText = false;

        // Internals
        private string creatorName = "";

        private string service = "";
        private bool singlePage = false;
        private int totalPosts = 0;
        private int downloadedFiles = 0;
        private int pages = 0; // Page 0 = Page 1 on coomer

        #endregion Variables

        #region Functions

        private string SanitizeText(string text)
        {
            return text.Replace("&#39;", "'").Replace(".", "").Replace(":", "(colon)").Replace("?", "").Replace("’", "'").Replace("\n", " ").Replace("\"", "").Replace("*", "");
        }

        private void ScrapePage(int page, int numberOfPostsToGet)
        {
            var client = new RestClient(creatorUrl);
            var request = new RestRequest().AddParameter("o", page * 50); // Page parameter calculation
            RestResponse response = client.GetAsync(request).Result;
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response.Content);
            if (totalPosts == 0)
            {
                // Fills the totalPosts variable if it hasn't been filled already
                HtmlNode totalPostsNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/main/section/div[1]/small");
                if (totalPostsNode != null)
                {
                    totalPosts = Int32.Parse(totalPostsNode.InnerText.Replace("Showing 1 - 50 of ", ""));
                }
            }

            HtmlNode? postsContainer = htmlDoc.DocumentNode.Descendants().Where(x => x.HasClass("card-list__items") && x.Name == "div").FirstOrDefault(); // Fetch the container that holds the posts
            int count = 0;
            foreach (HtmlNode post in postsContainer.ChildNodes)
            {
                if (post.NodeType != HtmlNodeType.Element) // We don't want text or comments
                {
                    continue;
                }
                if (count >= numberOfPostsToGet) // We want to cut off the loop when the threshold is reached
                {
                    break;
                }
                HtmlNode? linkNode = post.ChildNodes.FirstOrDefault(x => x.Attributes["href"] != null); // Find first child node with a link attribute (only "a" nodes here)
                postUrls.Add("https://coomer.su" + linkNode.Attributes["href"].Value);
                count++;
            }
            Console.WriteLine(count + " post(s) found on Page #" + (page + 1));
        }

        private void DownloadContent(string url, string parentFolder, string fileName)
        {
            if (!Directory.Exists(parentFolder))
            {
                Directory.CreateDirectory(parentFolder);
            }

            var pauseTokenSource = new PauseTokenSource();
            var cancelTokenSource = new CancellationTokenSource();
            var octaneEngine = new Engine();

            octaneEngine.DownloadFile(url, parentFolder + "/" + fileName, pauseTokenSource, cancelTokenSource).Wait(cancelTokenSource.Token);
            downloadedFiles++;
        }

        private void ScrapePost(string postUrl, int iteration)
        {
            int reverseNumber = (totalPosts - (iteration - 1)); // Cursed integer math
            var postClient = new RestClient(postUrl);
            var request = new RestRequest();
            RestResponse response = postClient.GetAsync(request).Result;
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response.Content);

            HtmlNode? titleParent = htmlDoc.DocumentNode.Descendants().Where(x => x.HasClass("post__title") && x.Name == "h1").FirstOrDefault();
            HtmlNode? titleSpan = titleParent.ChildNodes.Where(x => x.Name == "span").FirstOrDefault();
            string postTitle = SanitizeText(titleSpan.InnerText); // Sanitize post titles

            if (translateText)
            {
                var translation = translator.TranslateAsync(postTitle, "en").Result;
                postTitle = translation.Translation.Replace("/", " "); // Strip "/" to stop pathing issues
            }

            if (postSubfolders)
            {
                if (!Directory.Exists(savePath + "/" + creatorName + "/" + postTitle + " (Post #" + reverseNumber + ")"))
                {
                    Directory.CreateDirectory(savePath + "/" + creatorName + "/" + postTitle + " (Post #" + reverseNumber + ")");
                }
            }

            // Image posts
            HtmlNode? imagesNode = htmlDoc.DocumentNode.Descendants().Where(x => x.HasClass("post__files") && x.Name == "div").FirstOrDefault();
            if (imagesNode != null)
            {
                foreach (HtmlNode post in imagesNode.ChildNodes)
                {
                    if (post.NodeType != HtmlNodeType.Element) // We don't want text or comments
                    {
                        continue;
                    }
                    HtmlNode? linkNode = post.ChildNodes.FirstOrDefault(x => x.Attributes["href"] != null); // Find first child node with a link attribute (only "a" nodes here)
                    if (postSubfolders)
                    {
                        DownloadContent(linkNode.Attributes["href"].Value, savePath + "/" + creatorName + "/" + postTitle + " (Post #" + reverseNumber + ")", linkNode.Attributes["download"].Value);
                    }
                    else
                    {
                        DownloadContent(linkNode.Attributes["href"].Value, savePath + "/" + creatorName, linkNode.Attributes["download"].Value);
                    }
                }
            }

            // Video/File posts
            HtmlNode? videosNode = htmlDoc.DocumentNode.Descendants().Where(x => x.HasClass("post__attachments") && x.Name == "ul").FirstOrDefault();
            if (videosNode != null)
            {
                foreach (HtmlNode post in videosNode.ChildNodes)
                {
                    if (post.NodeType != HtmlNodeType.Element) // We don't want text or comments
                    {
                        continue;
                    }
                    HtmlNode? linkNode = post.ChildNodes.FirstOrDefault(x => x.Attributes["href"] != null); // Find first child node with a link attribute (only a nodes here)
                    if (postSubfolders)
                    {
                        DownloadContent(linkNode.Attributes["href"].Value, savePath + "/" + creatorName + "/" + postTitle + " (Post #" + reverseNumber + ")", linkNode.Attributes["download"].Value);
                    }
                    else
                    {
                        DownloadContent(linkNode.Attributes["href"].Value, savePath + "/" + creatorName, linkNode.Attributes["download"].Value);
                    }
                }
            }

            // Text only posts
            if (videosNode == null && imagesNode == null)
            {
                HtmlNode? contentNode = htmlDoc.DocumentNode.Descendants().Where(x => x.HasClass("post__content") && x.Name == "div").FirstOrDefault();
                if (contentNode != null)
                {
                    HtmlNode? message = contentNode.ChildNodes.FirstOrDefault(x => x.NodeType == HtmlNodeType.Element);
                    string postId = postUrl.Replace("https://coomer.su/" + service + "/user/" + creatorName + "/post/", "");
                    Console.WriteLine("Downloading text-only post into " + postId + ".txt...");
                    if (postSubfolders)
                    {
                        File.WriteAllText(savePath + "/" + creatorName + "/" + postTitle + " (Post #" + reverseNumber + ")" + "/" + postId + ".txt", message.InnerText);
                    }
                    else
                    {
                        File.WriteAllText(savePath + "/" + creatorName + "/" + postId + ".txt", message.InnerText);
                    }
                }
            }
        }

        private void DoProgressBarStep()
        {
            this.postProcessBar.PerformStep();
        }

        private void DisableBoxes()
        {
            this.urlBox.Enabled = false;
            this.postNumBox.Enabled = false;
            this.scrapeButton.Enabled = false;
        }

        private void EnableBoxes()
        {
            this.urlBox.Enabled = true;
            this.postNumBox.Enabled = true;
            this.scrapeButton.Enabled = true;
        }

        #endregion Functions

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        // Prompt user to select an output directory
        private void outputDirButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderPicker = new FolderBrowserDialog();
            DialogResult result = folderPicker.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderPicker.SelectedPath))
            {
                this.outputDirBox.Text = folderPicker.SelectedPath;
            }
        }

        // This is the post subfolders control, winforms just sucks
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.postSubfoldersCheck.Checked)
            {
                postSubfolders = true;
            }
            else
            {
                postSubfolders = false;
            }
        }

        // Translations control
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (this.translateCheck.Checked)
            {
                translateText = true;
            }
            else
            {
                translateText = false;
            }
        }

        private void urlBox_EnterPressed(object sender, KeyEventArgs e)
        {
            if (this.urlBox.Text == "")
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                creatorUrl = this.urlBox.Text;
                this.ActiveControl = null;
            }
        }

        private void urlBox_FocusLost(object sender, EventArgs e)
        {
            if (this.urlBox.Text == "")
            {
                return;
            }
            creatorUrl = this.urlBox.Text;
            this.ActiveControl = null;
        }

        private void numberBox_EnterPressed(object sender, KeyEventArgs e)
        {
            if (this.postNumBox.Text == "")
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    numberOfPosts = Int32.Parse(this.postNumBox.Text);
                    numberOfPosts_static = Int32.Parse(this.postNumBox.Text);
                    this.ActiveControl = null;
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Invalid number of posts!", "Error");
                    this.postNumBox.Text = "";
                }
            }
        }

        private void numberBox_FocusLost(object sender, EventArgs e)
        {
            if (this.postNumBox.Text == "")
            {
                return;
            }
            try
            {
                numberOfPosts = Int32.Parse(this.postNumBox.Text);
                numberOfPosts_static = Int32.Parse(this.postNumBox.Text);
                this.ActiveControl = null;
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid number of posts!", "Error");
                this.postNumBox.Text = "";
            }
        }

        private void outputDirBox_TextChanged(object sender, EventArgs e)
        {
            savePath = this.outputDirBox.Text;
        }

        private void scrapeButton_Click(object sender, EventArgs e)
        {
            if (savePath == "")
            {
                MessageBox.Show("Please select an output directory!", "Error");
                return;
            }
            if (creatorUrl == "")
            {
                MessageBox.Show("Please specify a creator URL!", "Error");
                return;
            }
            if (numberOfPosts == -1)
            {
                MessageBox.Show("Please specify the number of posts!", "Error");
                return;
            }

            var client = new RestClient(creatorUrl);
            DisableBoxes();

            // Set creator name & service
            service = creatorUrl.Contains("onlyfans") ? "onlyfans" : "fansly";
            creatorName = creatorUrl.Replace("https://coomer.su/" + service + "/user/", "");

            // Folder Creation
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            if (!Directory.Exists(savePath + "/" + creatorName))
            {
                Directory.CreateDirectory(savePath + "/" + creatorName);
            }

            #region Page Parsing Logic

            if (numberOfPosts != 0)
            {
                // Posts integer is a defined value, so separation code goes here
                if (numberOfPosts <= 50)
                {
                    // Posts integer only requires 1 page
                    pages = 0;
                    singlePage = true;
                }
                else
                {
                    // Posts integer requires more than 1 page
                    pages = (int)Math.Floor((float)(numberOfPosts / 50));
                    numberOfPosts = numberOfPosts % 50;
                }
            }
            else
            {
                // Posts integer is ambiguous and fetches all posts
                var request = new RestRequest().AddParameter("o", "0"); // Start posts offset at 0
                RestResponse response = client.GetAsync(request).Result;
                var HtmlDoc = new HtmlDocument();
                HtmlDoc.LoadHtml(response.Content);
                HtmlNode totalPostsNode = HtmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/main/section/div[1]/small"); // Text element that displays under the creator's banner at the top
                if (totalPostsNode != null)
                {
                    totalPosts = Int32.Parse(totalPostsNode.InnerText.Replace("Showing 1 - 50 of ", "")); // Parse out the total number of posts
                    pages = (int)Math.Floor((float)(totalPosts / 50));
                    numberOfPosts = totalPosts % 50;
                    numberOfPosts_static = totalPosts;
                }
                else
                {
                    // Total posts element doesn't appear if there is only 1 page, so that logic is
                    // handled here
                    pages = 0;
                    List<HtmlNode> posts = new List<HtmlNode>();
                    HtmlNode postsList = HtmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/main/section/div[3]/div[2]");
                    foreach (HtmlNode post in postsList.ChildNodes)
                    {
                        if (post.NodeType == HtmlNodeType.Element)
                        {
                            posts.Add(post);
                        }
                    }
                    numberOfPosts = posts.Count;
                    numberOfPosts_static = posts.Count;
                    totalPosts = posts.Count;
                }
            }

            #endregion Page Parsing Logic

            this.postProcessBar.Value = 1;
            this.postProcessBar.Maximum = numberOfPosts_static;
            this.postProcessBar.Minimum = 1;
            this.postProcessBar.Step = 1;

            // Thank god for multithreading!
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                // Full Page Scraper
                if (pages == 0)
                {
                    // Used if only grabbing the first page
                    ScrapePage(0, numberOfPosts);
                }
                else
                {
                    // Used for everything else
                    for (int i = 0; i < pages; i++)
                    {
                        ScrapePage(i, 50);
                    }
                }

                // Partial page scraper, used for scraping the final page from the series
                if (numberOfPosts > 0 && !singlePage)
                {
                    ScrapePage(pages, numberOfPosts);
                }

                // Begin parsing and downloading the posts
                for (int i = 0; i < postUrls.Count; i++)
                {
                    ScrapePost(postUrls[i], i + 1);
                    this.Invoke(DoProgressBarStep);
                }

                this.Invoke(EnableBoxes);
            }).Start();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}