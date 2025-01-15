using System.Drawing;
using FileSenderRailway;

namespace TagCloud.Visualisers;

public interface IBitmapCreator
{
    public Result<Bitmap> GenerateImage(IEnumerable<TagCloudWord> cloudWords, ImageCreateSettings settings);
}