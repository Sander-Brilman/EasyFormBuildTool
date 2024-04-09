namespace EzFromBuildTool.Parser;
public class CssParserSettings
{
    public required string[] CssFiles { get; init; }

    public required string OutputDirectory { get; init; }

    public required string FinalResultFileName { get; init; }

    public required bool SaveBuildProcessSteps { get; init; }



    public required string CssNonInternalVariableNewPrefix { get; init; }

    public required string CssInternalVariableOldPrefix { get; set; }

    public required string CssInternalVariableNewPrefix { get; init; }

    public required int CssInternalVariableRandomCharacterCount { get; set; }
}
