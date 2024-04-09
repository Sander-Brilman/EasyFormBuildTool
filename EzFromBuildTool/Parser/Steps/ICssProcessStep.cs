using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzFromBuildTool.Parser.Steps;
internal interface ICssProcessStep
{
    Task<string> ProcessCss(string cssCode, CssParserSettings settings); 
}
