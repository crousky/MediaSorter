using CommandLine;
using MediaSorter;
using MediaSorter.Media;
using Directory = System.IO.Directory;

Parser.Default.ParseArguments<SorterOptions>(args)
    .WithParsed(options =>
    {
        var actions = new List<string>();
        if (!string.IsNullOrEmpty(options.Metadata))
        {
            var metadata = MediaDetailParser.GetMetadata(options.Metadata);
            actions.AddRange(metadata);
        }
        else
        {
            Console.WriteLine($"From {options.Source} to {options.Destination}");
            var files = MediaLocator.GetMediaFilesRecursive(options.Source);
            foreach (var file in files)
            {
                var fileDestination = MediaLocator.GetDestinationPath(options.Destination, file);
                var sameLocation = file == fileDestination;
                if (sameLocation) continue;
                var fileExists = File.Exists(fileDestination);
                if (!fileExists || options.Overwrite)
                {
                    if (!options.Simulate)
                    {
                        var destinationDirectory = Path.GetDirectoryName(fileDestination);
                        if (!Directory.Exists(destinationDirectory))
                        {
                            Directory.CreateDirectory(destinationDirectory);
                        }

                        if (options.Move)
                        {
                            File.Move(file, fileDestination, options.Overwrite);
                        }
                        else
                        {
                            File.Copy(file, fileDestination, options.Overwrite);
                        }
                    }

                    var action = $"{file} --> {fileDestination}";
                    actions.Add(action);
                    Console.WriteLine(action);
                }
            }
        }

        if (!string.IsNullOrEmpty(options.LogFilePath))
        {
            File.WriteAllLines(options.LogFilePath, actions);
        }
    });