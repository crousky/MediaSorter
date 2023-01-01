namespace MediaSorter.Media;

public static class MediaLocator
{
    public static IEnumerable<string> GetMediaFiles(string directoryPath, List<string>? extensions = null)
    {
        var fileExtensions = extensions ?? MediaFileExtensions.ImageExtensions;
        var files = Directory.EnumerateFiles(directoryPath)
            .Where(f => fileExtensions.Contains(Path.GetExtension(f).ToLower()));
        foreach (var file in files)
        {
            yield return Path.GetFullPath(file);
        }
    }

    public static IEnumerable<string> GetMediaFilesRecursive(string directoryPath, List<string>? extensions = null)
    {
        var directories = Directory.GetDirectories(directoryPath);
        foreach (var directory in directories)
        {
            var directoryFiles = GetMediaFilesRecursive(directory, extensions);
            foreach (var directoryFile in directoryFiles)
            {
                yield return directoryFile;
            }
        }

        var mediaFiles = GetMediaFiles(directoryPath, extensions);
        foreach (var mediaFile in mediaFiles)
        {
            yield return mediaFile;
        }
    }

    public static string GetDestinationPath(string destination, string file)
    {
        var fileName = Path.GetFileName(file);
        var fileCreationDate = MediaDetailParser.GetFileDate(file);
        var destinationPath = Path.Combine(destination, 
            fileCreationDate.ToString("yyyy/MM/dd"),
            fileName);
        return Path.GetFullPath(destinationPath);
    }
}