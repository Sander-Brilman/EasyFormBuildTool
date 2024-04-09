using EzFromBuildTool.Parser.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzFromBuildTool.Parser;
internal class CssParserPipeline
{
    public static CssParserPipeline Build(CssParserSettings settings)
    {
        string cssCode = "";
        foreach (string cssFile in settings.CssFiles)
        {
            cssCode +=
                $""""
                
                /*
                [= EasyFormBuildTool =]
                Source code from file `{cssFile}`
                */

                """" +
                File.ReadAllText(cssFile);
        }

        return new CssParserPipeline(settings, cssCode);
    }

    private CssParserPipeline(CssParserSettings settings, string cssCode) => (_settings, _cssCode) = (settings, cssCode);




    private readonly CssParserSettings _settings;

    private string _cssCode;

    private readonly List<KeyValuePair<string, ICssProcessStep>> _steps = [];

    public CssParserPipeline AddStep<TStep>(string stepName)
        where TStep : ICssProcessStep
    {
        _steps.Add(new(stepName, Activator.CreateInstance<TStep>()));

        return this;
    }

    public async Task RunAsync()
    {
        int stepCount = 1;
        foreach ((string stepName, ICssProcessStep step) in _steps)
        {
            Console.WriteLine($"Processing step `{stepName}`");

            _cssCode = await step.ProcessCss(_cssCode, _settings);

            Console.WriteLine($"Finished step `{stepName}`");

            if (_settings.SaveBuildProcessSteps)
            {
                string fileName = $"EasyFormsBuildTool_Step_{stepCount}_{stepName}.css";

                Console.WriteLine($"Saving step `{stepName}` result as `{fileName}` in output directory");
                SaveCssInOutputDirectory(fileName);
            }

            Console.WriteLine();

            stepCount++;
        }

        Console.WriteLine($"Saving final result as `{_settings.FinalResultFileName}` in output directory");
        SaveCssInOutputDirectory(_settings.FinalResultFileName);
    }

    private void SaveCssInOutputDirectory(string fileName)
    {
        File.WriteAllText(Path.Combine(_settings.OutputDirectory, fileName), _cssCode);
    }
}
