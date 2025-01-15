using System.ComponentModel.DataAnnotations;
using Autofac;
using ConsoleClient.Services;
using ConsoleClient.CustomAttributes;
using FileSenderRailway;
using McMaster.Extensions.CommandLineUtils;

namespace ConsoleClient;

[MaxFontSize()]
public class CommandLineOptions
{
    public static int Main(string[] args) => CommandLineApplication.Execute<CommandLineOptions>(args);

    [MinLength(1, ErrorMessage = "Path to read file should be not null or empty")]
    [Option(Description = "Path to file with words for reading", ShortName = "r", LongName = "pathToWordFile")]
    public string? PathToWordFile { get; } = "file.txt";

    [MinLength(1, ErrorMessage = "Path to save file should be not null or empty")]
    [Option(Description = "Full path for save image", ShortName = "s", LongName = "pathToSaveImage")]
    public string? PathToSaveImage { get; } = "tagcloud.jpeg";

    [Range(1, 10_000, ErrorMessage = "Width should be in the range from 1 to 10_000")]
    [Option(Description = "Tag cloud image width", ShortName = "w", LongName = "imageWidth")]
    public int ImageWidth { get; } = 1200;

    [Range(1, 10_000, ErrorMessage = "Height should be in the range from 1 to 10_000")]
    [Option(Description = "Tag cloud image height", ShortName = "h", LongName = "imageHeight")]
    public int ImageHeight { get; } = 1200;

    [MinLength(1, ErrorMessage = "Background color should be not null or empty")]
    [Option(Description = "Image background color", ShortName = "bc", LongName = "backgroundColor")]
    public string? BackgroundColor { get; } = "white";

    [MinLength(1, ErrorMessage = "Word color should be not null or empty")]
    [Option(Description = "Tag cloud words color", ShortName = "wc", LongName = "wordColor")]
    public string? WordColor { get; } = "black";

    [MinLength(1, ErrorMessage = "FontFamily should be not null or empty")]
    [Option(Description = "Words font family", ShortName = "ff", LongName = "fontFamily")]
    public string? FontFamily { get; } = "Times New Roman";

    [Range(1, 200, ErrorMessage = "MinFontSize should be in range 1 to 200")]
    [Option(Description = "Words font min size", ShortName = "fmin", LongName = "fontMinSize")]
    public int MinFontSize { get; } = 12;

    [Range(1, 200, ErrorMessage = "MaxFontSize should be in range 1 to 200")]
    [Option(Description = "Words font max size", ShortName = "fmax", LongName = "fontMaxSize")]
    public int MaxFontSize { get; } = 64;

    [RegularExpression("^[cts]$", ErrorMessage = "Invalid spiral generator type. Use 'c', 't', or 's'.")]
    [Option(Description = "Spiral generator type", ShortName = "sg", LongName = "spiralGeneratorType")]
    public string? spiralGeneratorString { get; } = "c";

    // Метод для вывода значений параметров
    public void DisplayOptions()
    {
        Console.WriteLine("Принятые параметры:");
        Console.WriteLine($"- PathToWordFile: {PathToWordFile}");
        Console.WriteLine($"- PathToSaveImage: {PathToSaveImage}");
        Console.WriteLine($"- ImageWidth: {ImageWidth}");
        Console.WriteLine($"- ImageHeight: {ImageHeight}");
        Console.WriteLine($"- BackgroundColor: {BackgroundColor}");
        Console.WriteLine($"- WordColor: {WordColor}");
        Console.WriteLine($"- FontFamily: {FontFamily}");
        Console.WriteLine($"- MinFontSize: {MinFontSize}");
        Console.WriteLine($"- MaxFontSize: {MaxFontSize}");
        Console.WriteLine($"- SpiralGeneratorType: {spiralGeneratorString}");
    }

    private void OnExecute()
    {
        var settingsProvider = new SettingsProvider(this);

        var settings = settingsProvider.GetSettingsStorage();

        var container = settings.Then(DiRegister.RegisterAll);

        var createCloudImageResult = container
            .Then(container => container.Resolve<TagCloudImageCreator>())
            .Then(creator => creator.CreateCloudImage(
                settings.GetValueOrThrow().ImageSave,
                settings.GetValueOrThrow().ImageCreate,
                settings.GetValueOrThrow().ReaderSettings));

        if (!createCloudImageResult.IsSuccess)
        {
            Console.ForegroundColor = ConsoleColor.Red; // Устанавливаем красный цвет
            Console.WriteLine();
            Console.WriteLine("Tag cloud creation error(s)");
            Console.WriteLine(createCloudImageResult.Error);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green; // Устанавливаем зеленый цвет
            Console.WriteLine();
            Console.WriteLine("The tag cloud was created successfully");
            Console.WriteLine($"Image location: {this.PathToSaveImage}");
        }

        Console.ResetColor();
    }
}