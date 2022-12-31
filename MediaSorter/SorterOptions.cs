using CommandLine;

namespace MediaSorter;

public class SorterOptions
{
    [Option('s', "source", Required = true, HelpText = "Source directory of photos")]
    public string Source { get; set; }
    
    [Option('d', "destination", Required = true, HelpText = "Destination directory for photos")]
    public string Destination { get; set; }
}