using Orobouros.Bases;
using Orobouros.Managers;
using Orobouros.Tools.Web;
using ReaLTaiizor.Controls;
using System.Text.RegularExpressions;
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
                foreach (ProcessedScrapeData scrapeData in data.Content)
                {
                    Post post = (Post)scrapeData.Value;
                    Invoke(() => probeNameLabel.Text = defaultTitleText + post.Title);
                    Invoke(() => probeCommentLabel.Text = defaultCommentsText + post.Comments.Count.ToString());
                    Invoke(() => probeAttachmentLabel.Text = defaultAttachmentsText + post.Attachments.Count.ToString());
                    Invoke(() => probeAuthorLabel.Text = defaultAuthorText + post.Author.Username);
                    Invoke(() => probeUploadTimeLabel.Text = defaultUploadText + post.UploadDate.ToString());
                }
                Invoke(EnableBoxes);
            }).Start();
        }
    }
}