﻿using CommandLine;

namespace MediaSorter;

public class SorterOptions
{
    [Option('s', "source", Required = true, HelpText = "Source directory of photos")]
    public string Source { get; set; } = "";

    [Option('d', "destination", Required = true, HelpText = "Destination directory for photos")]
    public string Destination { get; set; } = "";
    
    [Option("destination-format", Required = false, HelpText = "DateTime ToString format used for the destination folder structure. Default value: yyyy/MM/dd")]
    public string DestinationFormat { get; set; } = "yyyy/MM/dd";
    
    [Option('m', "move", Required = false, HelpText = "Move files instead of copy")]
    public bool Move { get; set; }
    
    [Option("overwrite", Required = false, HelpText = "Copy or Move files that already exist")]
    public bool Overwrite { get; set; }
    
    [Option("simulate", Required = false, HelpText = "Run without making changes")]
    public bool Simulate { get; set; }
    
    [Option('l', "log", Required = false, HelpText = "Create log file")]
    public string LogFilePath { get; set; }
    
    [Option("metadata", Required = false, HelpText = "Display metadata about a file")]
    public string Metadata { get; set; }
    
    [Option('e', "extensions", Required = false, HelpText = "File extensions to include separated by ','. Ex: .jpg,.png", Separator = ',')]
    public IEnumerable<string> Extensions { get; set; } = new List<string>();
}