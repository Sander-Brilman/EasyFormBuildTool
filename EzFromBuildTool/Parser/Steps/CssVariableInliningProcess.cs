using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VStyle.Parser;
using VStyle.Parser.Steps;

namespace VStyleBuildTool.Parser.Steps;
internal class CssVariableInliningProcess : ICssProcessStep
{
    public async Task<string> ProcessCss(string cssCode, CssParserSettings settings)
    {
        string[] lines = cssCode.Split('\n');

        List<string> linesToRemove = [];

        // detect lines such as 
        // --some-variable: initial;
        // that can be removed (given those only occour in the :root)
        foreach (string line in lines)
        {
            if (line.TrimStart().StartsWith("--") is false)
            {
                continue;
            }

            string[] parts = line.Replace(";", "").Trim().Split(":");

            if (parts.Length <= 1)
            {
                continue;
            }

            string valueOnRight = parts[1].Trim();
            if (valueOnRight == "initial")
            {
                linesToRemove.Add(line);
            }
        }


        // remove detected lines
        foreach (string line in linesToRemove)
        {
            cssCode = cssCode.Replace(line, "");
        }


        return cssCode;
    }
}
