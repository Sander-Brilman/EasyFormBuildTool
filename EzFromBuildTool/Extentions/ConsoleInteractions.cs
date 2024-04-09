using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VStyle.Extentions;
public static class ConsoleInteractions
{
    public static string PromptString(string question, string? defaultValueIfLeftEmpty)
    {
        Console.WriteLine();
        Console.WriteLine(question);

        string? input;
        do
        {
            input = Console.ReadLine();

            if (defaultValueIfLeftEmpty is not null)
            {
                return defaultValueIfLeftEmpty;
            }
        }
        while (input is null);

        return input;
    }

    public static bool PromptYesNo(string question)
    {
        Console.WriteLine();
        Console.WriteLine($"{question} [y/n]");

        string? input;
        do
        {
            input = Console.ReadLine()?.ToLower();
        }
        while (input is null || (input != "y" && input != "n"));

        return input == "y";
    }
}
