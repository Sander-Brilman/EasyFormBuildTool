using System.Security.Cryptography;

namespace VStyle.Extentions;
internal static class RandomExtentions
{
    private static readonly HashSet<string> _usedVariables = [];

    private static readonly char[] _validCssVariableCharacters = "12345678990qwertyuiopasdfghjklzxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM-_".ToCharArray();

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
            if (count > 5000)// failsafe to prevent an endless loop when all possible combinations have been used
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

