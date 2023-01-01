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
            if (options.Simulate) Console.WriteLine("*** Using simulation mode - no changes will be made ***");
            Console.WriteLine($"{(options.Move ? "Moving" : "Copying")} from {options.Source} to {options.Destination}");
            var filePaths = MediaLocator.GetMediaFilesRecursive(options.Source, 
                options.Extensions.Any() ? options.Extensions.ToList() : null);
            var mediaFiles = new List<MediaFile>();
            foreach (var filePath in filePaths)
            {
                var mediaFile = new MediaFile(filePath) { DestinationPath = MediaLocator.GetDestinationPath(options.Destination, filePath, options.DestinationFormat) };
                if (filePath == mediaFile.DestinationPath)
                {
                    mediaFile.ActionRequired = false;
                }
                else if (File.Exists(mediaFile.DestinationPath) && !options.Overwrite)
                {
                    mediaFile.ActionRequired = false;
                }
                mediaFiles.Add(mediaFile);
            }
            Console.WriteLine($"Found {mediaFiles.Count} in the source directory, {mediaFiles.Count(m => m.ActionRequired)} will be {(options.Move ? "moved" : "copied")}");
            foreach (var file in mediaFiles.Where(f => f.ActionRequired))
            {
                if (!options.Simulate)
                {
                    var destinationDirectory = Path.GetDirectoryName(file.DestinationPath);
                    if (!Directory.Exists(destinationDirectory))
                    {
                        Directory.CreateDirectory(destinationDirectory);
                    }

                    if (options.Move)
                    {
                        File.Move(file.FilePath, file.DestinationPath, options.Overwrite);
                    }
                    else
                    {
                        File.Copy(file.FilePath, file.DestinationPath, options.Overwrite);
                    }
                }

                var action = $"{(options.Move ? "Moved" : "Copied")} {file.FilePath} --> {file.DestinationPath}";
                actions.Add(action);
                Console.WriteLine(action);
            }
        }

        if (!string.IsNullOrEmpty(options.LogFilePath))
        {
            File.WriteAllLines(options.LogFilePath, actions);
        }
    });