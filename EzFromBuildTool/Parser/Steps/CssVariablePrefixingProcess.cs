using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFormStyles.Parser.Steps;
internal class CssVariablePrefixingProcess : ICssProcessStep
{
    public async Task<string> ProcessCss(string cssCode, CssParserSettings settings)
    {
        if (settings.CssNonInternalVariableNewPrefix == "")
        {
            return cssCode;
        }

        return cssCode;
    }
}
