using System.Globalization;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace MediaSorter.Media;

public static class MediaDetailParser
{
    public static DateTime GetCreatedDate(string filePath)
    {
        var metadataDirectories = ImageMetadataReader.ReadMetadata(filePath);
        var subIfdDirectory = metadataDirectories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
        var dateTime = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);
        if (!string.IsNullOrEmpty(dateTime) && DateTime.TryParseExact(dateTime, "yyyy:MM:dd HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None, out var parsedDate)) return parsedDate;
        var fileInfo = new FileInfo(filePath);
        return fileInfo.CreationTime;
    }
}