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
            var builder = new ContainerBuilder();

            DiRegister.RegisterAll(builder, settings);
            var container = builder.Build();

            var createCloudImageResult = Result.Of(() => container.Resolve<TagCloudImageCreator>())
                .Then(creator => 
                    creator.CreateCloudImage(settings.ImageSave, settings.ImageCreate, settings.ReaderSettings));

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
            //try
            //{
            //    var tagCloudImageCreator = container.Resolve<TagCloudImageCreator>();
            //    tagCloudImageCreator.CreateCloudImage(settings.ImageSave, settings.ImageCreate, settings.ReaderSettings);
            //    Console.WriteLine("Облако тегов успешно создано.");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Произошла ошибка при создании облака тегов: {ex.Message}");
            //}
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