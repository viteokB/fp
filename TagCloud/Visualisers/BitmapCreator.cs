using System.Drawing;
using System.Drawing.Text;
using FileSenderRailway;

namespace TagCloud.Visualisers;

public class BitmapCreator : IBitmapCreator
{
    public Result<Bitmap> GenerateImage(IEnumerable<TagCloudWord> cloudWords, ImageCreateSettings settings)
    {
        if (cloudWords == null)
            return Result.Fail<Bitmap>("cloudWords argument cannot be null");
        if (settings == null)
            return Result.Fail<Bitmap>("settings argument cannot be null");

        return CreateBitmap(settings)
            .Then(bitmap => SetImageBackground(bitmap, settings.BackgroundColor))
            .Then(bitmapGraphics => WriteWordsOnImage(bitmapGraphics, cloudWords, settings));
    }


    private Result<Bitmap> CreateBitmap(ImageCreateSettings settings)
    {
        if (settings.ImageSize.Width <= 0)
            return Result.Fail<Bitmap>("Width of the bitmap image must be positive");
        if (settings.ImageSize.Height <= 0)
            return Result.Fail<Bitmap>("Height of the bitmap image must be positive");

        return new Bitmap(
            settings.ImageSize.Width,
            settings.ImageSize.Height);
    }

    private Result<Bitmap> SetImageBackground(Bitmap bitmap, Color color)
    {
        return Result.Of(() =>
        {
            using var graphics = Graphics.FromImage(bitmap);

            graphics.Clear(color);

            return bitmap;
        });
    }

    private Result<Bitmap> WriteWordsOnImage(Bitmap bitmap,
        IEnumerable<TagCloudWord> cloudWords,
        ImageCreateSettings settings)
    {
        return Result.Of(() =>
        {
            using var graphics = Graphics.FromImage(bitmap);

            var wordsX = new SortedSet<int>();
            var wordsY = new SortedSet<int>();

            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            using var brush = new SolidBrush(settings.WordColor);

            foreach (var tagWord in cloudWords)
            {
                using var font = new Font(settings.FontFamily, tagWord.FontSize);

                AddWordCoordinates(wordsX, wordsY, tagWord.Box);
                graphics.DrawString(tagWord.TextWord, font, brush, tagWord.Box.Location);
            }

            if (!AreWordsInImageBoundaries(wordsX, wordsY, settings))
            {
                throw new ArgumentException("The words go beyond the boundaries of the image size," +
                                            "\nSet bigger values");
            }

            return bitmap;
        });
    }

    private void AddWordCoordinates(SortedSet<int> wordsX, SortedSet<int> wordsY, Rectangle rectangle)
    {
        wordsX.Add(rectangle.X); //minx

        wordsX.Add(rectangle.X + rectangle.Width); //maxX

        wordsY.Add(rectangle.Y); //minY

        wordsY.Add(rectangle.Y + rectangle.Height); //maxY
    }

    private bool AreWordsInImageBoundaries(SortedSet<int> wordsX, SortedSet<int> wordsY, ImageCreateSettings settings)
    {
        return wordsX.Min >= 0 && wordsY.Min >= 0 &&
               wordsX.Max <= settings.ImageSize.Width &&
               wordsY.Max <= settings.ImageSize.Height;
    }


    private record BitmapGraphics(Graphics Graphics, Bitmap Bitmap);
}