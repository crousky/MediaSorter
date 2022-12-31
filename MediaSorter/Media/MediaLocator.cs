namespace MediaSorter.Media;

public static class MediaLocator
{
    public static IEnumerable<string> GetMediaFiles(string directoryPath)
    {
        var files = Directory.EnumerateFiles(directoryPath)
            .Where(f => MediaFileExtensions.ImageExtensions.Contains(Path.GetExtension(f).ToLower()));
        foreach (var file in files)
        {
            yield return file;
        }
    }

    public static IEnumerable<string> GetMediaFilesRecursive(string directoryPath)
    {
        var directories = Directory.GetDirectories(directoryPath);
        foreach (var directory in directories)
        {
            var directoryFiles = GetMediaFilesRecursive(directory);
            foreach (var directoryFile in directoryFiles)
            {
                yield return directoryFile;
            }
        }

        var mediaFiles = GetMediaFiles(directoryPath);
        foreach (var mediaFile in mediaFiles)
        {
            yield return mediaFile;
        }
    }
}