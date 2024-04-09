using System.Security.Cryptography;

namespace EzFromBuildTool.Extentions;
internal static class RandomExtentions
{
    private static readonly HashSet<string> _usedVariables = [];

    private static readonly char[] _validCssVariableCharacters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM-_".ToCharArray();

    private static string RandomVariableSafeString(int length)
    {
        return new string(RandomNumberGenerator.GetItems<char>(_validCssVariableCharacters, length));
    }

    public static string RandomCssVariableName(this Random random, string prefix, int numberOfRandomCharacters)
    {
        string variable;

        int count = 0;
        do
        {
            if (count > 5000)// failsafe
            {
                throw new InvalidOperationException("Ran out of unqiue strings for css variables, increase the limit in the settings");
            }

            variable = $"--{prefix}{RandomVariableSafeString(numberOfRandomCharacters)}";
            count++;
        }
        while (_usedVariables.Contains(variable));

        return variable;
    }
}
