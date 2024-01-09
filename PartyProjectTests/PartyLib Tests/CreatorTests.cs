using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyLib;
using PartyLib.Bases;

namespace PartyProjectTests.PartyLib_Tests
{
    [TestClass]
    public class CreatorTests
    {
        private string coomerTestCreator = "https://coomer.su/onlyfans/user/belledelphine";
        private string kemonoTestCreator = "https://kemono.su/patreon/user/3161935";

        /// <summary>
        /// Tests a basic coomer creator fetch
        /// </summary>
        [TestMethod]
        public void Coomer_Creator_Fetch()
        {
            Creator creator = new Creator(coomerTestCreator);
            Assert.IsTrue(creator.SuccessfulFetch);
        }

        /// <summary>
        /// Tests a basic kemono creator fetch
        /// </summary>
        [TestMethod]
        public void Kemono_Creator_Fetch()
        {
            Creator creator = new Creator(kemonoTestCreator);
            Assert.IsTrue(creator.SuccessfulFetch);
        }

        /// <summary>
        /// Tests a profile picture fetch from a coomer creator
        /// </summary>
        [TestMethod]
        public void Coomer_Creator_Fetch_ProfilePicture()
        {
            Creator creator = new Creator(coomerTestCreator);
            if (creator.SuccessfulFetch)
            {
                Image? creatorProfilePic = creator.GetProfilePicture();
                Assert.IsNotNull(creatorProfilePic);
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests a profile picture fetch from a kemono creator
        /// </summary>
        [TestMethod]
        public void Kemono_Creator_Fetch_ProfilePicture()
        {
            Creator creator = new Creator(kemonoTestCreator);
            if (creator.SuccessfulFetch)
            {
                Image? creatorProfilePic = creator.GetProfilePicture();
                Assert.IsNotNull(creatorProfilePic);
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests a background picture fetch from a coomer creator
        /// </summary>
        [TestMethod]
        public void Coomer_Creator_Fetch_BackgroundPicture()
        {
            Creator creator = new Creator(coomerTestCreator);
            if (creator.SuccessfulFetch)
            {
                Image? creatorProfilePic = creator.GetProfileBanner();
                Assert.IsNotNull(creatorProfilePic);
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests a background picture fetch from a kemono creator
        /// </summary>
        [TestMethod]
        public void Kemono_Creator_Fetch_BackgroundPicture()
        {
            Creator creator = new Creator(kemonoTestCreator);
            if (creator.SuccessfulFetch)
            {
                Image? creatorProfilePic = creator.GetProfileBanner();
                Assert.IsNotNull(creatorProfilePic);
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests a background picture URL fetch from a kemono creator
        /// </summary>
        [TestMethod]
        public void Kemono_Creator_Fetch_BackgroundPicture_URL()
        {
            Creator creator = new Creator(kemonoTestCreator);
            if (creator.SuccessfulFetch)
            {
                string? creatorProfilePic = creator.GetProfileBannerURL();
                Assert.IsNotNull(creatorProfilePic);
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests a background picture URL fetch from a coomer creator
        /// </summary>
        [TestMethod]
        public void Coomer_Creator_Fetch_BackgroundPicture_URL()
        {
            Creator creator = new Creator(coomerTestCreator);
            if (creator.SuccessfulFetch)
            {
                string? creatorProfilePic = creator.GetProfileBannerURL();
                Assert.IsNotNull(creatorProfilePic);
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests a profile picture URL fetch from a coomer creator
        /// </summary>
        [TestMethod]
        public void Coomer_Creator_Fetch_ProfilePicture_URL()
        {
            Creator creator = new Creator(coomerTestCreator);
            if (creator.SuccessfulFetch)
            {
                string? creatorProfilePic = creator.GetProfilePictureURL();
                Assert.IsNotNull(creatorProfilePic);
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests a profile picture URL fetch from a kemono creator
        /// </summary>
        [TestMethod]
        public void Kemono_Creator_Fetch_ProfilePicture_URL()
        {
            Creator creator = new Creator(kemonoTestCreator);
            if (creator.SuccessfulFetch)
            {
                string? creatorProfilePic = creator.GetProfilePictureURL();
                Assert.IsNotNull(creatorProfilePic);
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests a fetch of a kemono creator's total posts
        /// </summary>
        [TestMethod]
        public void Kemono_Creator_Fetch_Total_Posts()
        {
            Creator creator = new Creator(kemonoTestCreator);
            if (creator.SuccessfulFetch)
            {
                int? totalPosts = creator.GetTotalPosts();
                Assert.IsNotNull(totalPosts);
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests a fetch of a coomer creator's total posts
        /// </summary>
        [TestMethod]
        public void Coomer_Creator_Fetch_Total_Posts()
        {
            Creator creator = new Creator(coomerTestCreator);
            if (creator.SuccessfulFetch)
            {
                int? totalPosts = creator.GetTotalPosts();
                Assert.IsNotNull(totalPosts);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}