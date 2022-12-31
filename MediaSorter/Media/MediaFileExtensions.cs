namespace MediaSorter.Media;

public static class MediaFileExtensions
{
    public static IReadOnlyList<string> ImageExtensions { get; } 
        = new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".raw", ".arw", ".cr2", ".srf", ".sr2", ".tiff" }.AsReadOnly();

    public static IReadOnlyList<string> VideoExtensions { get; } 
        = new List<string> { ".mp4" }.AsReadOnly();

    public static IReadOnlyList<string> AllMediaExtensions
    {
        get
        {
            var allExtensions = new List<string>();
            allExtensions.AddRange(ImageExtensions);
            allExtensions.AddRange(VideoExtensions);
            return allExtensions.AsReadOnly();
        }
    }
}