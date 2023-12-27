### **Coomer.su is currently undergoing a restructuring and some creators MAY NOT WORK! If the scraper suddenly closes itself, it means the creator is in the process of being transferred. This is an issue with their servers and I cannot do anything about it except maybe provide error handling to tell you about it. PLEASE DO NOT REPORT THIS AS A BUG!**

**WARNING: Usage of this program & the PartySites (kemono.su & coomer.su) may be illegal in your country/jurisdiction. I am not responsible for any misuse of this program or any illicit activity. This program is provided "as-is" and I do not endorse nor support software piracy.**

![PartyScraper v0.6.0-pre](https://i.imgur.com/EKJlvdM.png)

# PartyLib + GUI Project
PartyLib is a bulk downloading tool for the archiving sites kemono.su & coomer.su with many advanced features & tools.

# Features
- Support for all supported archiving sites (except discord - pending support)
- Automatic text translation toggle
- Post subfolder organization toggle
- Post subfolder numerical ID toggle
- Mega.nz automatic downloading support (requires proxies and probably [proxifier](https://proxifier.com/)) --- **WARNING: MEGA support module directly violates MEGA's ToS! By using this feature, you acknowledge you may face consequences!**
- Randomized user agent to avoid telemetry
- No Selenium used! Backend runs on raw HTML parsing and is thus extremely fast
- A GUI that actually tells you what everything does (mostly)
- Runs on PartyLib, and is therefore entirely portable. You can't use it on Linux though, because for some reason they don't have the ``Image`` class and I cannot be bothered to add support (may add at some point).
- Balanced scraping speed to ease load on partysite servers
- Efficient HTTP caching
- Configurable download threads + chunk sizes
- Persistent configs
- Custom language translation language support
- Log-rich output for diagnosing errors
- Built-in utility tools
- Multithreading & useful thread-safe event handlers

# GUI Usage
Just fill in all the values, it's really simple.

### Options
**IMPORTANT:** The number of posts starts from the most recent creator post and goes backward. Inputting "0" can be used to scrape the entirety of the Creator's library. Example: "50" would scrape the creator's 50 most recent posts. Both PartySites organize their posts with the most recent creator post being first in the carousel.

**(OPTIONAL)** Number of chunks is how many binary chunks you want the download split into. The default is 1 and shouldn't be changed unless you know what you're doing.

**(OPTIONAL)** Number of threads/connections is how many concurrent connections to download a file with. The default is 8. Any number above 8 causes increasingly quick rate-limiting, so changing this number is strongly unadvised.

The output directory is pretty self-explanatory. Keep in mind a subfolder will be created here with the scraped's creators username.

The creator URL is self-explanatory. Input the Creator's PartySites landing page (the one you see when first clicking on their profile). Example: https://kemono.su/patreon/user/foobar

**(OPTIONAL)** The Post Subfolders option is whether to create a new folder for every post. It is highly recommended to keep this on, as otherwise, you will have a sea of loose files.

**(OPTIONAL)** The Download Descriptions option is whether to download post descriptions. This one is based on personal preference and can be changed if desired.

**(OPTIONAL)** The Subfolder # Suffix option is whether to append the post's number onto the end of the folder. Example: FooBar Post (Post #300)

**(OPTIONAL)** The Download MEGA Links option allows you, the user, to practice the dark arts of programming. Enabling this option allows you to download any MEGA links a creator has inside their post, using a combination of cursed MEGA software and black magic. You NEED proxies for this and you need to rotate them often. Use Proxifier.

**(OPTIONAL)** The Translate Post Titles option is self-explanatory.

**(OPTIONAL)** The Translate Post Descriptions option is self-explanatory. **WARNING: THIS WILL HEAVILY INCREASE YOUR TRANSLATION API USAGE AND THERE IS A HIGH CHANCE OF RATE-LIMITING! ENABLING THIS IS STRONG ILL-ADVISED!**

**(OPTIONAL)** The Disable GIFs option disables the space-filling gifs I've placed around the application. Enabling this is definitive proof that you do not enjoy having fun.

**(OPTIONAL)** The Discord Rich Presence option is a cursed abomination, brewed from the deepest and darkest magic. Enabling this will announce to your friends on Discord that you are a degenerate, and will 100% get you banned from every public server due to NSFW content (if they can somehow zoom in on the unfathomably tiny icon).

### Buttons
**Kill MEGA Threads** - Most likely will never need to be used. This button kills all MegaCMD background processes in case of a canceled download/other error with the software.

**Scrape Posts** - Attempts to invade Yugoslavia

**Various "Browse" Buttons** - Opens a file/folder explorer based on what the textbox next to it requires.

**Test Translation API** - Ensures you can make translation requests. If this fails, you must wait 24 hours or get a VPN.

## Misc
**Creator MEGA Password** - Yeah honestly this should never have to be used. This is only included due to an option in MegaCMD and possible niche usage. I do not support this and cannot help with it as I honestly have no idea what it is.

**Localization Code** - Used by the translation service to specify what language to translate text into. Uses ISO 639-1.


### UI
The topmost progress bar represents the current downloading progress for the currently processed file.

The middle progress bar represents how many attachments have been scraped already.

The bottom progress bar represents how many posts have been scraped so far.

# PartyLib Usage
Documentation has been mainly written with XML annotations that describe mostly what each function does and why. I may eventually make a small wiki if it gets complex enough.

# PartyTest Usage
Programmers may notice a 3rd project called "PartyTest" included in the solution. This is a VERY simple CLI applet designed to debug PartyLib and can be deleted if desired. I have kept it inside the main branch in case anybody would like to see how the core of PartyLib functions.

# Building
The GUI is a ``.NET 6 Core`` WinForms project and requires everything those projects require. Past that, it has a few NuGet dependencies which should get resolved on startup. ``dotnet restore`` can also be run if this doesn't happen for some reason.

PartyLib is a ``.NET 6 Core`` Class Library project and is designed to have only a few dependencies. These can be restored with ``dotnet restore`` if Visual Studio does not automatically find them.

Building is done via standard build tools and outputs to the standard ``bin`` directory. An important fact to keep in mind is that the PartyScraper GUI project is stored in the ``KemonoScraperSharp-GUI`` folder as this was the deprecated name and I am unsure how to change it within Visual Studio.

You may also simply check out ``Actions`` and download any artifacts you wish, pre-compiled.

# PartyLib Information 
The PartyLib project is now included in this repo! PartyLib is a C# .NET 6 class library I created to make the core functions of the scraping software portable, mainly to allow me to program the GUIs more easily. This library can however also be used for anyone wishing to create their scraper or modify data from party websites.

# Planned Features
- ~~Option to translate into any language instead of just English~~ **Implemented into PartyLib, pending GUI addition**
- Discord archiver support
- ~~Advanced options (num of file parts to download, num retries)~~ **Implemented into PartyLib, pending GUI addition**
- ~~PartyLib multithreading (if applicable anywhere)~~ **Scrapped in favor of the programmer opting into multithreading with their code**
- ~~Various misc data sources~~ **Implemented in v0.6.0 as the output log**
- ~~Custom user agent setting~~ **Reworked into randomized user agent**
- ~~Config file support~~ **Added in v0.5.1**
- Oldest posts -> newest posts option instead of the reverse, which is currently the default
- Scrape posts from a specific offset (e.g. scrape 50 posts back starting from the Creator's 30th recent post)
- ~~Better error handling for GUI~~ **Implemented in v0.6.0**
- ~~PartyLib documentation~~ **XML Annotations added**
- ~~MEGA command window hidden option~~ **Implemented in v0.6.0, option deprecated in favor of minimized window**

# Afterword
This, along with the PartySites project, are tools I made because I was bored and the code reflects as such. The code is mostly efficient, but is by no means highly optimized and bugs are likely. I may refactor the code at a future point, but as of right now it is functional and decently fast.

