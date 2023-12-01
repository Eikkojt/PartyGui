**WARNING: Usage of this program & the PartySites (kemono.su & coomer.su) may be illegal in your country/jurisdiction. I am not responsible for any misuse of this program or any illicit activity. This program is provided "as-is" and I do not endorse nor support software piracy.**

# PartyLib + GUI Project

This will replace my old private CLI tools for all future work.

# Features
- Support for all supported archiving sites (except discord)
- Automatic text translation toggle
- Post subfolder organization toggle
- Post subfolder numerical ID toggle
- Mega.nz automatic downloading support (requires proxies)
- Randomized user agent to avoid telemetry
- No Selenium used! Backend runs on raw HTML parsing and is thus extremely fast
- A GUI that actually tells you what everything does (mostly)
- Runs on PartyLib, and is therefore entirely portable. You can't use it on Linux though, because for some reason they don't have the ``Image`` class and I cannot be bothered to add support (may add at some point).

# GUI Usage
Just fill in all the values, it's really simple.

Number of posts starts from the most recent creator post and goes backwards. Inputting "0" can be used to scrape the entirety of the Creator's library.

The topmost progress bar represents how many attachments have finished downloading for the currently scraped post.

The bottommost progress bar represents how many posts have been scraped so far.

# PartyLib Usage
Documentation has been mainly written with XML annotations that describe mostly what each function does and why. I may eventually make a small wiki if it gets complex enough.

# PartyTest Usage
Programmers may notice a 3rd project called "PartyTest" included in the solution. This is a VERY simple CLI applet designed to debug PartyLib and can be deleted if desired. I have kept it inside the main branch in case anybody would like to see how the core of PartyLib functions.

# Building
The GUI is a ``.NET 6 Core`` WinForms project and requires everything those require. Past that, it has a few NuGet dependencies which should get resolved on startup. ``dotnet restore`` can also be run if this doesn't happen for some reason.

PartyLib is a ``.NET 6 Core`` Class Library project and is designed to have only a few dependencies. These can be restored with ``dotnet restore`` if Visual Studio does not automatically find them.

Building is done via standard build tools and outputs to the standard ``bin`` directory.

You may also simply check out ``Actions`` and download any artifacts you wish, pre-compiled.

# PartyLib Information 
The PartyLib project is now included in this repo! PartyLib is a C# .NET 6 class library I created to make the core functions of the scraping software portable, mainly to allow me to program the GUIs easier. This library can however also be used for anyone wishing to create their own scraper or modify data from party websites.

# Planned Features
- ~~Option to translate into any language instead of just English~~ **Implemented into PartyLib, pending GUI addition**
- Discord archiver support
- ~~Advanced options (num of file parts to download, num retries)~~ **Implemented into PartyLib, pending GUI addition**
- ~~PartyLib multithreading (if applicable anywhere)~~ **Scrapped in favor of the programmer opting into multithreading with their own code**
- Various misc data sources
- ~~Custom user agent setting~~ **Reworked into randomized user agent**
- ~~Config file support~~ **Added in v0.5.1**
- Oldest posts -> newest posts option instead of the reverse, which is currently the default
- Scrape posts from a specific offset (e.g. scrape 50 posts back starting from the Creator's 30th recent post)
- Better error handling for GUI
- ~~PartyLib documentation~~ **XML Annotations added**

# Afterword
This, along with the PartySites project, are tools I made because I was bored and the code reflects as such. The code is mostly efficient, but is by no means highly optimized and bugs are likely. I may refactor the code at a future point, but as of right now it is functional and decently fast.

