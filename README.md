# MediaSorter

MediaSorter helps you organize media files by sorting them into folders

## Parameters
`example: -s "G:/DCIM" -d "D:/Photo Shoots" --destination-format yyyy/MM/dd`

-s, --source Required. Source directory of photos

-d, --destination Required. Destination directory for photos

--destination-format DateTime ToString format used for the destination folder structure. Default value: yyyy/MM/dd

-m, --move Move files instead of copy

--overwrite Copy or Move files that already exist

--simulate Run without making changes

-l, --log Create log file

--metadata Display metadata about a file

-e, --extensions File extensions to include separated by ','. Ex: .jpg,.png

--help Display this help screen.

--version Display version information.