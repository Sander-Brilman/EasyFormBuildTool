using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFormStyles.Parser.Steps;
internal class MinifyCssProccess : ICssProcessStep
{
    public async Task<string> ProcessCss(string cssCode, CssParserSettings settings)
    {
        var url = "https://www.toptal.com/developers/cssminifier/api/raw";

        using HttpClient client = new();

        var content = new FormUrlEncodedContent(
        [
            new KeyValuePair<string, string>("input", cssCode)
        ]);

        var response = await client.PostAsync(url, content);

        if (response.IsSuccessStatusCode is false)
        {
            throw new InvalidOperationException("Minify api responded with status code: " + response.StatusCode);
        }

        cssCode = await response.Content.ReadAsStringAsync();

        return cssCode;
    }
}
