using BitmapSavers;
using FileSenderRailway;
using TagCloud;
using TagCloud.Visualisers;
using WordHandlers.Interfaces;
using WordReaders.Factory;
using WordReaders.Interfaces;
using WordReaders.Settings;

namespace ConsoleClient.Services;

public class TagCloudImageCreator(
    BitmapSaver bitmapSaver,
    IMultiFormatReader multiFormatReader,
    IWordCounter wordsCounter,
    TagCloudBitmapCreator tagCloudCreator)
{
    public Result<None> CreateCloudImage(
        ImageSaveSettings imageSaveSettings,
        ImageCreateSettings imageCreateSettings,
        WordReaderSettings wordReaderSettings)
    {
        return multiFormatReader.Read(wordReaderSettings)
            .Then(wordsCounter.CountWords)
            .Then(dictCountWords =>
                tagCloudCreator.CreateTagCloudBitmap(dictCountWords, imageCreateSettings))
            .Then(bitmap => bitmapSaver.Save(imageSaveSettings, bitmap));
    }
}