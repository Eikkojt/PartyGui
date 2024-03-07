using Orobouros.Bases;
using Orobouros.Managers;
using Orobouros.Tools.Web;
using System.Text.RegularExpressions;
using static Orobouros.OrobourosInformation;

namespace PartyScraper3._0
{
    public partial class Main : Form
    {
        public string defaultTitleText { get; set; }
        public string defaultAttachmentsText { get; set; }
        public string defaultCommentsText { get; set; }

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
            // Clear old data
            probeListBox.Items.Clear();
            attachmentsList.Images.Clear();
            probeNameLabel.Text = defaultTitleText;
            probeCommentLabel.Text = defaultCommentsText;
            probeAttachmentLabel.Text = defaultAttachmentsText;

            // Begin scrape
            List<ModuleContent> requestedInfo = new List<ModuleContent> { ModuleContent.Subposts };
            ModuleData? data = ScrapingManager.ScrapeURL(CreatorURL, requestedInfo, 1);
            foreach (ProcessedScrapeData scrapeData in data.Content)
            {
                Post post = (Post)scrapeData.Value;
                probeNameLabel.Text = defaultTitleText + post.Title;
                probeCommentLabel.Text = defaultCommentsText + post.Comments.Count.ToString();
                probeAttachmentLabel.Text = defaultAttachmentsText + post.Attachments.Count.ToString();

                int count = 0;
                foreach (Attachment attach in post.Attachments)
                {
                    if (attach.AttachmentType == AttachmentContent.Image)
                    {
                        Image img = Image.FromStream(attach.Binary);
                        attachmentsList.Images.Add(img);
                        ListViewItem item = new ListViewItem();
                        item.Text = attach.Name;
                        item.ImageIndex = count;
                        probeListBox.Items.Add(item);
                        count++;
                    }
                }
            }
        }
    }
}