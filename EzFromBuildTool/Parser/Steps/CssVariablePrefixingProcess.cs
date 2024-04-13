using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VStyle.Parser.Steps;
internal class CssVariablePrefixingProcess : ICssProcessStep
{
    public async Task<string> ProcessCss(string cssCode, CssParserSettings settings)
    {
        if (settings.CssNonInternalVariableNewPrefix == "")
        {
            return cssCode;
        }

        cssCode = cssCode.Replace("--", $"--{settings.CssNonInternalVariableNewPrefix}");


        // revert changes made to internal variables
        cssCode = cssCode.Replace(settings.CssNonInternalVariableNewPrefix + settings.CssInternalVariableOldPrefix, settings.CssInternalVariableOldPrefix);


        return cssCode;
    }
}
