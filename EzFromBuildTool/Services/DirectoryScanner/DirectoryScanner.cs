using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VStyle.Services.Scanner;
internal class DirectoryScanner
{
    private static string[] GetRecursiveFilesFromDirectory(string directory)
    {
        List<string> files = [.. Directory.GetFiles(directory)];

        foreach (string subDirectory in Directory.EnumerateDirectories(directory))
        {
            files.AddRange(GetRecursiveFilesFromDirectory(subDirectory));
        }

        return [.. files];
    }

    public static string[] FindAllFilesInDirectoryWithExtentions(string directoryPath, string fileExtention, bool recursiveSearch)
    {
        if (Directory.Exists(directoryPath) is false)
        {
            return [];
        }

        if (fileExtention.StartsWith('.') is false)
        {
            fileExtention = $".{fileExtention}";
        }

        string[] files = recursiveSearch
            ? GetRecursiveFilesFromDirectory(directoryPath)
            : Directory.GetFiles(directoryPath);

        return files
            .Where(file => Path.GetExtension(file) == fileExtention)
            .ToArray();
    }
}
