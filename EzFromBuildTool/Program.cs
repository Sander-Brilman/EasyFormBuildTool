using Cocona;
using VStyle.Parser;
using VStyle.Parser.Steps;
using VStyle.Results;
using VStyle.Services.Scanner;
using VStyle.Services.SettingsSerivce;
using VStyle.Services.Validator;
using Microsoft.Extensions.DependencyInjection;
using OneOf;
using OneOf.Types;
using static VStyle.Extentions.ConsoleInteractions;
using static System.Console;


var builder = CoconaApp.CreateBuilder();

builder.Services.AddScoped<SettingsService>();
builder.Services.AddScoped<SettingsValidator>();

var app = builder.Build();

app.Run(async (SettingsService settingsService, SettingsValidator validator) =>
{
    if (settingsService.IsSettingsFileAvailable() is false)
    {
        WriteLine($"No settings file with the name {SettingsService.SettingsFileName} is in the current directory");
        bool createFile = PromptYesNo("Would you like to generate an example settings file");

        if (createFile)
        {
            await settingsService.GenerateExampleSettingsFile();
            WriteLine($"\nExample settings file with name {SettingsService.SettingsFileName} was generated in the current directory\nEdit the settings then re-run the program\n\n");
        }

        return;
    }

    WriteLine("Loading settings..");
    CssParserSettings? settings = await settingsService.GetBuildSettingsFromFile();

    if (settings is null) { return; }

    WriteLine("Validating settings..");
    OneOf<Success, ErrorMessage> result = validator.ValidateSettings(settings);

    if (result.IsT1)
    {
        WriteLine("\nError in settings: " + result.AsT1.Message);
        return;
    }

    WriteLine("\n[== Begin processing ==]\n");

    
    await CssParserPipeline
        .Build(settings)
        .AddStep<CssVariablePrefixingProcess>("non-internal variable prefixing")
        .AddStep<CssInternalVariableRenamingProccess>("Internal variable renaming")
        .AddStep<CssSelectorNestingProccess>("selector nesting")
        .AddStep<MinifyCssProccess>("Minifing")
        .RunAsync();
});