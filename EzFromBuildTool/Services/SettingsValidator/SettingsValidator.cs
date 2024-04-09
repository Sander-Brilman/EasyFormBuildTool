using EzFromBuildTool.Parser;
using EzFromBuildTool.Results;
using OneOf;
using OneOf.Types;

namespace EzFromBuildTool.Services.Validator;
public class SettingsValidator
{
    public OneOf<Success, ErrorMessage> ValidateSettings(CssParserSettings settings)
    {
        if (Directory.Exists(settings.OutputDirectory) is false)
        {
            return new ErrorMessage($"Output directory does not exist");
        }

        foreach (string file in settings.CssFiles)
        {
            if (File.Exists(file) is false)
            {
                return new ErrorMessage($"Css source file `{file}` does not exist");
            }
        }

        if (settings.CssNonInternalVariableNewPrefix.StartsWith("--") || settings.CssInternalVariableNewPrefix.StartsWith("--"))
        {
            return new ErrorMessage($"Css variable prefixes dont need to contain the `--` at the start. this is automatically taken care of");
        }

        static bool IsValidFilename(string fileName) => 
            fileName.IndexOfAny(Path.GetInvalidFileNameChars()) == -1;
        
        if (IsValidFilename(settings.FinalResultFileName) is false)
        {
            return new ErrorMessage($"{nameof(settings.FinalResultFileName)} is not a valid filename");
        }

        return new Success();
    }
}
