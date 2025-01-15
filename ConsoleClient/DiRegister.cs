using Autofac;
using BitmapSavers;
using ConsoleClient.Services;
using FileSenderRailway;
using TagCloud;
using TagCloud.Factory;
using TagCloud.Interfaces;
using TagCloud.Visualisers;
using WordHandlers;
using WordHandlers.Handlers;
using WordHandlers.Interfaces;
using WordHandlers.WordCounters;
using WordReaders.Factory;
using WordReaders.Interfaces;
using WordReaders.Readers;

namespace ConsoleClient;

public static class DiRegister
{
    private static readonly bool isRegistred = false;

    public static IContainer RegisterAll(SettingsStorage settings)
    {
        if (isRegistred)
            throw new InvalidOperationException("RegisterAll can only be called one time");

        var builder = new ContainerBuilder();

        RegisterBitmapSaverClasses(builder);
        RegisterTagCloudClasses(builder);
        RegisterWordHandlersClasses(builder);
        RegisterWordReadersClasses(builder);
        RegisterByInstance(builder, settings);
        RegisterConsoleClientServices(builder);

        return builder.Build();
    }

    private static void RegisterConsoleClientServices(ContainerBuilder builder)
    {
        builder.RegisterType<TagCloudImageCreator>().AsSelf().SingleInstance();
    }

    private static void RegisterByInstance(ContainerBuilder builder, SettingsStorage settings)
    {
        var spiralFactory = new SpiralPointGeneratorFactory();
        builder.RegisterInstance(spiralFactory.CreateSpiralPointGenerator(settings.ImageCreate));
    }

    private static void RegisterBitmapSaverClasses(ContainerBuilder builder)
    {
        builder.RegisterType<BitmapSaver>().AsSelf().SingleInstance();
    }

    private static void RegisterTagCloudClasses(ContainerBuilder builder)
    {
        builder.RegisterType<TagCloudLayouter>().As<ICloudLayouter>();
        builder.RegisterType<TagCloudBitmapCreator>().AsSelf().SingleInstance();

        builder.RegisterType<BitmapCreator>().As<IBitmapCreator>();

        //SpiralPointGeneratorFactory в RegisterByInstance
    }

    private static void RegisterWordHandlersClasses(ContainerBuilder builder)
    {
        builder.RegisterType<LowercaseWordHandler>().As<IWordHandler>();
        builder.RegisterType<BoringWordHandler>().As<IWordHandler>();

        builder.Register<FilteredWordsCounter>(context =>
        {
            var wordHandlers = context.Resolve<IEnumerable<IWordHandler>>();
            var list = new List<Func<IEnumerable<string>, Result<IEnumerable<string>>>>();
            foreach (var wordHandler in wordHandlers) list.Add(wordHandler.ApplyWordHandler);
            return new FilteredWordsCounter(list);
        }).As<IWordCounter>();
    }

    private static void RegisterWordReadersClasses(ContainerBuilder builder)
    {
        builder.RegisterType<WordReaderFactory>().As<IWordReaderFactory>().SingleInstance();
        builder.RegisterType<MultiFormatWordReader>().As<IMultiFormatReader>().SingleInstance();
    }
}