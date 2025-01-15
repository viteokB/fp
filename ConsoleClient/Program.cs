using Autofac;
using CommandLine;
using ConsoleClient.Services;
using FileSenderRailway;

namespace ConsoleClient;

internal class Program
{
    private static void Main(string[] args)
    {
        var result = Parser.Default.ParseArguments<CommandLineOptions>(args);
        var options = result.Value;

        result.WithParsed(options =>
        {
            options.DisplayOptions();
            var settingsProvider = new SettingsProvider(options);

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
                Console.WriteLine($"Image location: {options.PathToSaveImage}");
            }

            Console.ResetColor();
        })
        .WithNotParsed(errors =>
        {
            Console.WriteLine("Ошибка при парсинге аргументов:");
            foreach (var error in errors)
            {
                Console.WriteLine($"- {error}");
            }
        });
    }
}