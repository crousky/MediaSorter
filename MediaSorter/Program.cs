using CommandLine;
using MetadataExtractor;
using MediaSorter;
using MediaSorter.Media;
using Directory = System.IO.Directory;

Parser.Default.ParseArguments<SorterOptions>(args)
    .WithParsed<SorterOptions>(o =>
    {
        Console.WriteLine($"From {o.Source} to {o.Destination}");
        var files = MediaLocator.GetMediaFilesRecursive(o.Source);
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            var fileCreateDate = MediaDetailParser.GetCreatedDate(file);
            var destination =
                Path.Combine(o.Destination, fileCreateDate.Year.ToString(), fileCreateDate.Month.ToString());
            Directory.CreateDirectory(destination);
            File.Copy(file, Path.Combine(destination, fileName));
            Console.WriteLine($"Copied {fileName}");
        }
    });