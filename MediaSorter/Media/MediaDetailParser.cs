using System.Globalization;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace MediaSorter.Media;

public static class MediaDetailParser
{
    public static DateTime GetFileDate(string filePath)
    {
        var metadataDirectories = ImageMetadataReader.ReadMetadata(filePath);
        var id0Directory = metadataDirectories.OfType<ExifIfd0Directory>().FirstOrDefault();
        var dateTime = id0Directory?.GetDescription(ExifDirectoryBase.TagDateTime);
        var subIfdDirectory = metadataDirectories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
        var dateTimeOriginal = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);
        var parsedDateTime = GetMin(ParseExifDateTime(dateTime), ParseExifDateTime(dateTimeOriginal));
        if (parsedDateTime.HasValue) return parsedDateTime.Value;
        var fileInfo = new FileInfo(filePath);
        return GetMin(GetMin(fileInfo.CreationTime, fileInfo.LastWriteTime), fileInfo.LastAccessTime);
    }

    private static DateTime? GetMin(DateTime? x, DateTime? y) => x == null && y == null ? null : GetMin(x ?? DateTime.MaxValue, y ?? DateTime.MaxValue);
    private static DateTime GetMin(DateTime x, DateTime y) => x < y ? x : y;
    
    private static DateTime? ParseExifDateTime(string? exifDateTime) =>
        string.IsNullOrEmpty(exifDateTime) ? null :
        DateTime.TryParseExact(exifDateTime, "yyyy:MM:dd HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None, out var parsedDate) ? parsedDate : null;

    public static IEnumerable<string> GetMetadata(string filePath)
    {
        var metadataDirectories = ImageMetadataReader.ReadMetadata(filePath);
        foreach (var directory in metadataDirectories)
        foreach (var tag in directory.Tags)
            yield return $"{directory.Name} - {tag.Name} = {tag.Description}";
    }
}