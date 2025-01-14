using BitmapSavers;
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
    public void CreateCloudImage(
        ImageSaveSettings imageSaveSettings,
        ImageCreateSettings imageCreateSettings,
        WordReaderSettings wordReaderSettings)
    {
        var fileWords = multiFormatReader.Read(wordReaderSettings); ;
        var dictCountWords = wordsCounter.CountWords(fileWords.GetValueOrThrow());

        var bitmap = tagCloudCreator.CreateTagCloudBitmap(dictCountWords.GetValueOrThrow(), imageCreateSettings);
        bitmapSaver.Save(imageSaveSettings, bitmap);
    }
}