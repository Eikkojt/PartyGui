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
            Assert.IsNotNull(creator);
        }

        /// <summary>
        /// Tests a basic kemono creator fetch
        /// </summary>
        [TestMethod]
        public void Kemono_Creator_Fetch()
        {
            Creator creator = new Creator(kemonoTestCreator);
            Assert.IsNotNull(creator);
        }

        /// <summary>
        /// Tests a profile picture fetch from a coomer creator
        /// </summary>
        [TestMethod]
        public void Coomer_Creator_Fetch_ProfilePicture()
        {
            Creator creator = new Creator(coomerTestCreator);
            if (creator != null)
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
            if (creator != null)
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
            if (creator != null)
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
            if (creator != null)
            {
                Image? creatorProfilePic = creator.GetProfileBanner();
                Assert.IsNotNull(creatorProfilePic);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}