using Microsoft.Win32.SafeHandles;
using Orobouros.Bases;
using Orobouros.Managers;
using Orobouros.Tools.Web;
using ReaLTaiizor.Controls;
using System.Text.RegularExpressions;
using Windows.Storage.Pickers;
using static Orobouros.OrobourosInformation;

namespace PartyScraper3._0
{
    public partial class Main : Form
    {
        public string defaultTitleText { get; set; }
        public string defaultAttachmentsText { get; set; }
        public string defaultCommentsText { get; set; }
        public string defaultAuthorText { get; set; }
        public string defaultUploadText { get; set; }

        private readonly Regex creatorUrlRegex = new Regex("https://[A-Za-z0-9]+\\.su/[A-Za-z0-9]+/user/[A-Za-z0-9]+");

        private string CreatorURL { get; set; } = String.Empty;

        private int NumberOfPosts { get; set; } = 0;

        private bool PostSubfolders { get; set; } = true;
        private bool DownloadDescriptions { get; set; } = false;
        private bool OverrideFileTime { get; set; } = true;
        private string OutputDirectory { get; set; }

        public Main()
        {
            InitializeComponent();
            this.Select();
        }

        private void ReleaseKeyboard(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void DisableBoxes()
        {
            scrapeButton.Enabled = false;
            probeCreatorButton.Enabled = false;
        }

        private void EnableBoxes()
        {
            scrapeButton.Enabled = true;
            probeCreatorButton.Enabled = true;
        }

        private void creatorTextbox_TextChanged(object sender, EventArgs e)
        {
            if (creatorTextbox.Text == "")
            {
                creatorTextbox.SetErrorState(false);
                return;
            }

            if (creatorUrlRegex.IsMatch(creatorTextbox.Text))
            {
                creatorTextbox.SetErrorState(false);
                CreatorURL = creatorTextbox.Text;
            }
            else
            {
                creatorTextbox.SetErrorState(true);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ScrapingManager.InitializeModules();
            defaultTitleText = probeNameLabel.Text;
            defaultAttachmentsText = probeAttachmentLabel.Text;
            defaultCommentsText = probeCommentLabel.Text;
            defaultAuthorText = probeAuthorLabel.Text;
            defaultUploadText = probeUploadTimeLabel.Text;
        }

        private void postNumberTextbox_TextChanged(object sender, EventArgs e)
        {
            if (postNumberTextbox.Text == "")
            {
                postNumberTextbox.SetErrorState(false);
                return;
            }

            try
            {
                int num = Int32.Parse(postNumberTextbox.Text);
                postNumberTextbox.SetErrorState(false);
                NumberOfPosts = num;
            }
            catch
            {
                postNumberTextbox.SetErrorState(true);
            }
        }

        private void subfolderSwitch_CheckedChanged(object sender, EventArgs e)
        {
            PostSubfolders = subfoldersSwitch.Checked;
        }

        private void downloadDescriptionsSwitch_CheckedChanged(object sender, EventArgs e)
        {
            DownloadDescriptions = downloadDescriptionsSwitch.Checked;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            ScrapingManager.FlushSupplementaryMethods();
        }

        private void probeCreatorButton_Click(object sender, EventArgs e)
        {
            if (CreatorURL == null || CreatorURL == String.Empty)
            {
                MessageBox.Show("Creator URL is empty! Please correct this issue to proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DisableBoxes();
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                // Clear old data
                Invoke(() => probeNameLabel.Text = defaultTitleText);
                Invoke(() => probeCommentLabel.Text = defaultCommentsText);
                Invoke(() => probeAttachmentLabel.Text = defaultAttachmentsText);
                Invoke(() => probeAuthorLabel.Text = defaultAuthorText);
                Invoke(() => probeUploadTimeLabel.Text = defaultUploadText);

                // Begin scrape
                List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Subposts };
                ModuleData? data = ScrapingManager.ScrapeURL(CreatorURL, requestedInfo, 1);

                if (data != null)
                {
                    foreach (ProcessedScrapeData scrapeData in data.Content)
                    {
                        Post post = (Post)scrapeData.Value;
                        Invoke(() => probeNameLabel.Text = defaultTitleText + post.Title);
                        Invoke(() => probeCommentLabel.Text = defaultCommentsText + post.Comments.Count.ToString());
                        Invoke(() => probeAttachmentLabel.Text = defaultAttachmentsText + post.Attachments.Count.ToString());
                        Invoke(() => probeAuthorLabel.Text = defaultAuthorText + post.Author.Username);
                        Invoke(() => probeUploadTimeLabel.Text = defaultUploadText + post.UploadDate.ToString());
                    }
                }
                Invoke(EnableBoxes);
            }).Start();
        }

        private void scrapeButton_Click(object sender, EventArgs e)
        {
            if (CreatorURL == null || CreatorURL == String.Empty)
            {
                MessageBox.Show("Creator URL is empty! Please correct this issue to proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (NumberOfPosts == 0 || NumberOfPosts < -1)
            {
                MessageBox.Show("Number of posts is invalid! Please correct this issue to proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (OutputDirectory == null || OutputDirectory == String.Empty)
            {
                MessageBox.Show("No output folder selected! Please correct this issue to proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DisableBoxes();
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Invoke(() =>
                {
                    outputProgress.Visible = true;
                    outputLabel.Text = "Fetching metadata from API...";
                });

                // Begin scrape
                List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Subposts };
                ModuleData? data = ScrapingManager.ScrapeURL(CreatorURL, requestedInfo, NumberOfPosts);
                if (data != null)
                {
                    Invoke(() =>
                    {
                        downloadProgressBar.Maximum = 100;
                        postsProgressBar.Maximum = data.Content.Count;
                        outputLabel.Text = "Beginning downloader setup...";
                    });

                    int iteration = 0;
                    DownloadManager downloader = new DownloadManager();
                    downloader.DownloadProgressed += Downloader_DownloadProgressed;
                    foreach (ProcessedScrapeData scrapeData in data.Content)
                    {
                        // Download the attachments
                        Post post = (Post)scrapeData.Value;
                        iteration++;

                        Invoke(() =>
                        {
                            downloadProgressBar.Value = 0;
                            attachmentsProgressBar.Value = 0;
                            postsProgressBar.PerformStep();
                            attachmentsProgressBar.Maximum = post.Attachments.Count;
                            outputLabel.Text = $"Downloading post #{iteration}/{data.Content.Count}";
                        });

                        string DownloadDir = String.Empty;

                        // Dynamically assign output dir
                        if (PostSubfolders)
                        {
                            DownloadDir = Path.Combine(OutputDirectory, post.Author.Username, StringManager.SanitizeText(post.Title));
                            if (!DownloadDescriptions && post.Attachments.Count == 0)
                            {
                                continue;
                            }
                            else
                            {
                                if (!Directory.Exists(DownloadDir))
                                {
                                    Directory.CreateDirectory(DownloadDir);
                                }
                            }
                        }
                        else
                        {
                            DownloadDir = Path.Combine(OutputDirectory, post.Author.Username);
                            if (!Directory.Exists(DownloadDir))
                            {
                                Directory.CreateDirectory(DownloadDir);
                            }
                        }

                        // Download descriptions
                        if (DownloadDescriptions)
                        {
                            File.WriteAllText(Path.Combine(DownloadDir, "description.txt"), post.Description);
                        }

                        // Begin downloading attachments
                        foreach (Attachment attach in post.Attachments)
                        {
                            Invoke(() =>
                            {
                                attachmentsProgressBar.PerformStep();
                            });

                            for (int i = 0; i < 5; i++)
                            {
                                Invoke(() =>
                                {
                                    downloadProgressBar.Value = 0;
                                });

                                bool success = downloader.DownloadContent(attach.URL, DownloadDir, attach.Name);
                                if (OverrideFileTime)
                                {
                                    SafeFileHandle handle = File.OpenHandle(Path.Combine(DownloadDir, attach.Name), FileMode.Open, FileAccess.ReadWrite);
                                    File.SetCreationTime(handle, (DateTime)attach.ParentPost.UploadDate);
                                    File.SetLastWriteTime(handle, (DateTime)attach.ParentPost.UploadDate);
                                    File.SetLastAccessTime(handle, (DateTime)attach.ParentPost.UploadDate);
                                    handle.Close();
                                }

                                if (success)
                                {
                                    break;
                                }
                            }
                        }

                        if (OverrideFileTime)
                        {
                            Directory.SetCreationTime(DownloadDir, (DateTime)post.UploadDate);
                            Directory.SetLastAccessTime(DownloadDir, (DateTime)post.UploadDate);
                            Directory.SetLastWriteTime(DownloadDir, (DateTime)post.UploadDate);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("PartyModule returned null data! This means a creator's page could not be scraped. Did you get ratelimited? Scraping task aborted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Invoke(() =>
                {
                    downloadProgressBar.Value = 0;
                    attachmentsProgressBar.Value = 0;
                    postsProgressBar.Value = 0;
                    outputProgress.Visible = false;
                    outputLabel.Text = "Idle...";
                });
                Invoke(EnableBoxes);
            }).Start();
        }

        private void Downloader_DownloadProgressed(object sender, Downloader.DownloadProgressChangedEventArgs eventArgs, string fileName = null)
        {
            Invoke(() =>
            {
                downloadProgressBar.PerformStep();
            });
        }

        private void browseOutputButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                outputFolderTextbox.Text = dialog.SelectedPath;
            }
        }

        private void outputFolderTextbox_TextChanged(object sender, EventArgs e)
        {
            OutputDirectory = outputFolderTextbox.Text;
        }

        private void modificationDateSwitch_CheckedChanged(object sender, EventArgs e)
        {
            OverrideFileTime = modificationDateSwitch.Checked;
        }
    }
}