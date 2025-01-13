using System.Drawing;

namespace TagCloud.Visualisers;

public interface IBitmapCreator
{
    public Bitmap GenerateImage(IEnumerable<TagCloudWord> cloudWords, ImageCreateSettings settings);
}