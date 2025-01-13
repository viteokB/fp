using System.Drawing;
using System.Drawing.Text;

namespace TagCloud.Visualisers;

public class BitmapCreator : IBitmapCreator
{
    public Bitmap GenerateImage(IEnumerable<TagCloudWord> cloudWords, ImageCreateSettings settings)
    {
        var bitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);

        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(settings.BackgroundColor);
        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        using var brush = new SolidBrush(settings.WordColor);

        foreach (var tagWord in cloudWords)
        {
            using var font = new Font(settings.FontFamily, tagWord.FontSize);

            graphics.DrawString(tagWord.TextWord, font, brush, tagWord.Box.Location);
        }

        return bitmap;
    }
}