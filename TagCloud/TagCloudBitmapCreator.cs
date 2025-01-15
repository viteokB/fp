using System.Drawing;
using FileSenderRailway;
using TagCloud.Interfaces;
using TagCloud.Visualisers;

namespace TagCloud;

public class TagCloudBitmapCreator
{
    private readonly ICloudLayouter layouter;

    private readonly IBitmapCreator bitmapCreator;

    public TagCloudBitmapCreator(ICloudLayouter layouter, IBitmapCreator creator)
    {
        this.layouter = layouter;
        this.bitmapCreator = creator;
    }

    public Result<Bitmap> CreateTagCloudBitmap(Dictionary<string, int> wordFreqDictionary, ImageCreateSettings settings)
    {
        if (wordFreqDictionary == null || wordFreqDictionary.Count == 0)
            return Result.Fail<Bitmap>("The word frequency dictionary cannot be null or empty.");

        var maxFreq = wordFreqDictionary.Values.Max();
        var cloudWords = new List<TagCloudWord>();

        foreach (var kvp in wordFreqDictionary)
        {
            var word = kvp.Key;
            var freq = kvp.Value;

            var fontSize = TransformFreqToSize(settings.FontMinSize, settings.FontMaxSize, freq, maxFreq);
            var rectangleSize = TextSize(word, fontSize, settings.FontFamily);

            var rectangle = layouter.PutNextRectangle(rectangleSize).GetValueOrThrow();
            cloudWords.Add(new TagCloudWord(rectangle, word, fontSize));
        }

        return bitmapCreator.GenerateImage(cloudWords, settings);
    }

    private int TransformFreqToSize(int minFontSize, int maxFontSize, int freq, int maxFreq)
    {
        return (int)(minFontSize + (float)freq / maxFreq * (maxFontSize - minFontSize));
    }

    private Size TextSize(string text, int fontSize, FontFamily fontFamily)
    {
        using var tempFont = new Font(fontFamily, fontSize);
        using var bitmap = new Bitmap(1, 1);
        using var graphics = Graphics.FromImage(bitmap);
        return graphics.MeasureString(text, tempFont).ToSize();
    }
}