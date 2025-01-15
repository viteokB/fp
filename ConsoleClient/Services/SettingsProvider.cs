using System.Drawing;
using System.Text;
using BitmapSavers;
using ConsoleClient.Interfaces;
using FileSenderRailway;
using TagCloud.SpiralPointGenerators.enums;
using TagCloud.Visualisers;
using WordReaders.Settings;

namespace ConsoleClient.Services;

public class SettingsProvider(CommandLineOptions options) : ISettingsProvider
{
    public Result<SettingsStorage> GetSettingsStorage()
    {
        return Result.Of(() =>
            new SettingsStorage(
                new ImageCreateSettings(
                    new Size(options.ImageWidth, options.ImageHeight),
                    Color.FromName(options.BackgroundColor),
                    new FontFamily(options.FontFamily),
                    options.MinFontSize,
                    options.MaxFontSize,
                    Color.FromName(options.WordColor),
                    DefineSpiralPointGeneratorsType(options.spiralGeneratorString).GetValueOrThrow()
                ),
                new ImageSaveSettings(options.PathToSaveImage),
                new WordReaderSettings(options.PathToWordFile, Encoding.UTF8)
            ));
    }

    public Result<SpiralPointGeneratorsType> DefineSpiralPointGeneratorsType(string str)
    {
        return str switch
        {
            "c" => SpiralPointGeneratorsType.Circular,
            "t" => SpiralPointGeneratorsType.Triangular,
            "s" => SpiralPointGeneratorsType.Square,
            _ => Result.Fail<SpiralPointGeneratorsType>($"Non-existing SpiralPointGenerator : {str}")
        };
    }
}