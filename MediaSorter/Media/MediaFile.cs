namespace MediaSorter.Media;

public class MediaFile
{
    public MediaFile(string filePath)
    {
        FilePath = filePath;
    }

    public string FilePath { get; set; }
    public string DestinationPath { get; set; }
    public bool ActionRequired { get; set; } = true;
}