using VFormStyles.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFormStyles.Parser.Steps;
internal class CssInternalVariableRenamingProccess : ICssProcessStep
{
    public async Task<string> ProcessCss(string cssCode, CssParserSettings settings)
    {
        string[] lines = cssCode.Split('\n');

        HashSet<string> internalVariables = [];

        // find all internal variabled
        foreach (string line in lines)
        {
            if (line.TrimStart().StartsWith($"--{settings.CssInternalVariableOldPrefix}") is false)
            {
                continue;
            }

            string internalVariable = line.Trim().Split(':')[0];

            if (internalVariables.Contains(internalVariable))
            {
                continue;
            }

            internalVariables.Add(internalVariable);
        }

        // replace all internal variables
        foreach (string internalVariable in internalVariables)
        {
            string replacement = Random.Shared.RandomCssVariableName(
                settings.CssInternalVariableNewPrefix,
                settings.CssInternalVariableRandomCharacterCount);

            cssCode = cssCode.Replace(internalVariable, replacement);
        }


        return cssCode;
    }
}
