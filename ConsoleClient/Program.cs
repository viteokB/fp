using Autofac;
using CommandLine;
using ConsoleClient.Services;

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

            try
            {
                var tagCloudImageCreator = container.Resolve<TagCloudImageCreator>();
                tagCloudImageCreator.CreateCloudImage(settings.ImageSave, settings.ImageCreate, settings.ReaderSettings);
                Console.WriteLine("Облако тегов успешно создано.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при создании облака тегов: {ex.Message}");
            }
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