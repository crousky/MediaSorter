namespace PhotoSorter.Interfaces;

public interface IMediaDetailParser
{
    DateTime GetCreatedDate(string filePath);
}