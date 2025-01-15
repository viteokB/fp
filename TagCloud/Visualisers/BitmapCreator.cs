using System.Drawing;
using System.Drawing.Text;
using FileSenderRailway;
using FluentAssertions;

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

        return Result.Ok(new Bitmap(
            settings.ImageSize.Width, 
            settings.ImageSize.Height));
    }

    private Result<BitmapGraphics> SetImageBackground(Bitmap bitmap, Color color)
    {
        var graphics = Graphics.FromImage(bitmap);

        return Result.Of(() =>
        {
            graphics.Clear(color);

            return new BitmapGraphics(graphics, bitmap);
        }).HandleOnFail(graphics.Dispose);
    }

    private Result<Bitmap> WriteWordsOnImage(BitmapGraphics bitmapGraphics, 
        IEnumerable<TagCloudWord> cloudWords,
        ImageCreateSettings settings)
    {
        return Result.Of(() =>
        {
            bitmapGraphics.Deconstruct(out var graphics, out var bitmap);

            using (graphics)
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                using var brush = new SolidBrush(settings.WordColor);

                foreach (var tagWord in cloudWords)
                {
                    using var font = new Font(settings.FontFamily, tagWord.FontSize);

                    graphics.DrawString(tagWord.TextWord, font, brush, tagWord.Box.Location);
                }
            }

            return bitmap;
        });
    }

    private record BitmapGraphics(Graphics Graphics, Bitmap Bitmap);
}