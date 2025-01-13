using CommandLine;

namespace ConsoleClient;

public class CommandLineOptions
{
    [Option('r', "pathToWordFile", Default = "file.txt", HelpText = "Path to file with words for tag cloud")]
    public string? PathToWordFile { get; set; }

    [Option('s', "pathToSaveImage", Default = "tagcloud.jpeg", HelpText = "Full path image saving")]
    public string? PathToSaveImage { get; set; }

    [Option('w', "imageWidth", Default = 1200, HelpText = "Image save width")]
    public int ImageWidth { get; set; }

    [Option('h', "imageHeight", Default = 1200, HelpText = "Image save height")]
    public int ImageHeight { get; set; }

    [Option('b', "backgroundColor", Default = "white", HelpText = "Image background color")]
    public string? BackgroundColor { get; set; }

    [Option('f', "wordColor", Default = "black", HelpText = "Word color")]
    public string? WordColor { get; set; }

    [Option("fontFamily", Default = "Times New Roman", HelpText = "FontFamily of tag cloud words")]
    public string? FontFamily { get; set; }

    [Option("fontMinSize", Default = 12, HelpText = "Min font size of tag cloud words")]
    public int MinFontSize { get; set; }

    [Option("fontMaxSize", Default = 64, HelpText = "Max font size of tag cloud words")]
    public int MaxFontSize { get; set; }

    [Option("spiralGeneratorType", Default = "c",
        HelpText = "SpiralGeneratorType, defines form of cloud (c - circular, t - triangular, s - square")]
    public string? spiralGeneratorString { get; set; }

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
}