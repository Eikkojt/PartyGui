using Orobouros.Managers;
using System.Text.RegularExpressions;

namespace PartyScraper3._0
{
    public partial class Main : Form
    {
        private readonly Regex creatorUrlRegex = new Regex("https://[A-Za-z0-9]+\\.su/[A-Za-z0-9]+/user/[A-Za-z0-9]+");

        private string CreatorURL { get; set; } = String.Empty;

        public Main()
        {
            InitializeComponent();
        }

        private void ReleaseKeyboard(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void creatorTextbox_TextChanged(object sender, EventArgs e)
        {
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
        }
    }
}