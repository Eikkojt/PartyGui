**WARNING: Usage of this program & the PartySites (kemono.su & coomer.su) may be illegal in your country/jurisdiction. I am not responsible for any misuse of this program or any illicit activity. This program is provided "as-is" and I do not endorse nor support software piracy.**

![PartyScraper v0.6.0-pre](https://i.imgur.com/EKJlvdM.png)

# PartyLib + GUI Project

This will replace my old private CLI tools for all future work.

# Features
- Support for all supported archiving sites (except discord - pending support)
- Automatic text translation toggle
- Post subfolder organization toggle
- Post subfolder numerical ID toggle
- Mega.nz automatic downloading support (requires proxies) --- **WARNING: MEGA support module directly violates MEGA's ToS! By using this feature, you acknowledge you may face consequences!**
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
Number of posts starts from the most recent creator post and goes backwards. Inputting "0" can be used to scrape the entirety of the Creator's library.

(OPTIONAL) Number of chunks is how many binary chunks you want the download split into. The default is 1 and shouldn't be changed unless you know what you're doing.

(OPTIONAL) Number of threads is how many concurrent connections to download a file with. Default is 8. Any number above 8 causes increasingly quick ratelimiting, so changing this number is strongly unadvised.

Output directory is pretty self explanatory. Keep in mind a subfolder will be created here with the scraped's creators username.

Creator URL is self explanatory. Input the Creator's PartySites landing page (the one you see when first clicking on their profile). Example: https://kemono.su/patreon/user/foobar

### UI
The topmost progress bar represents how many attachments have finished downloading for the currently scraped post.

The bottommost progress bar represents how many posts have been scraped so far.

# PartyLib Usage
Documentation has been mainly written with XML annotations that describe mostly what each function does and why. I may eventually make a small wiki if it gets complex enough.

# PartyTest Usage
Programmers may notice a 3rd project called "PartyTest" included in the solution. This is a VERY simple CLI applet designed to debug PartyLib and can be deleted if desired. I have kept it inside the main branch in case anybody would like to see how the core of PartyLib functions.

# Building
The GUI is a ``.NET 6 Core`` WinForms project and requires everything those required. Past that, it has a few NuGet dependencies which should get resolved on startup. ``dotnet restore`` can also be run if this doesn't happen for some reason.

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

