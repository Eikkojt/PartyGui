using PartyLib.Bases;
using PartyLib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyProjectTests.PartyLib_Tests
{
    [TestClass]
    public class ScraperHelperTests
    {
        // Init creators
        private static Creator coomerCreator { get; } = new Creator(CreatorTests.coomerTestCreator);

        private static Creator kemonoCreator { get; } = new Creator(CreatorTests.kemonoTestCreator);

        // Init helpers
        private static ScraperHelper coomerHelper { get; } = new ScraperHelper(coomerCreator, 0);

        private static ScraperHelper kemonoHelper { get; } = new ScraperHelper(kemonoCreator, 0);

        // Tests

        [TestMethod(displayName: "[Coomer] Scrape Page (10 posts)")]
        public void Coomer_Scrape_Page()
        {
            List<Post>? posts = coomerHelper.ScrapePage(0, 10);
            Assert.IsTrue(posts.Count == 10);
        }

        [TestMethod(displayName: "[Kemono] Scrape Page (10 posts)")]
        public void Kemono_Scrape_Page()
        {
            List<Post>? posts = kemonoHelper.ScrapePage(0, 10);
            Assert.IsTrue(posts.Count == 10);
        }

        [TestMethod(displayName: "[Coomer] Scrape Full Page")]
        public void Coomer_Scrape_Page_50()
        {
            List<Post>? posts = coomerHelper.ScrapePage(0, 50);
            Assert.IsTrue(posts.Count == 50);
        }

        [TestMethod(displayName: "[Kemono] Scrape Full Page")]
        public void Kemono_Scrape_Page_50()
        {
            List<Post>? posts = kemonoHelper.ScrapePage(0, 50);
            Assert.IsTrue(posts.Count == 50);
        }
    }
}