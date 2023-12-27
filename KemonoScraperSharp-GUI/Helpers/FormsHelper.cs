namespace KemonoScraperSharp_GUI.Helpers;

public static class FormsHelper
{
    public static Party_Main? MainForm { get; set; }
    public static ProgressBar? DownloadProgressBar { get; set; }
    public static ProgressBar? AttachmentsProgressBar { get; set; }
    public static ProgressBar? PostProgressBar { get; set; }

    public static void SetDownloadBar(int value)
    {
        if (DownloadProgressBar != null)
        {
            DownloadProgressBar.Value = value;
        }
    }

    public static void IncrementDownloadBar(int amount)
    {
        if (DownloadProgressBar != null)
        {
            DownloadProgressBar.Increment(amount);
        }
    }

    public static void SetAttachmentsBar(int value)
    {
        if (AttachmentsProgressBar != null)
        {
            AttachmentsProgressBar.Value = value;
        }
    }

    public static void IncrementAttachmentsBar(int amount)
    {
        if (AttachmentsProgressBar != null)
        {
            AttachmentsProgressBar.Increment(amount);
        }
    }

    public static void SetPostsBar(int value)
    {
        if (PostProgressBar != null)
        {
            PostProgressBar.Value = value;
        }
    }

    public static void IncrementPostsBar(int amount)
    {
        if (PostProgressBar != null)
        {
            PostProgressBar.Increment(amount);
        }
    }
}