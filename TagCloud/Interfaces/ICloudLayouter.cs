using System.Drawing;
using FileSenderRailway;

namespace TagCloud.Interfaces;

public interface ICloudLayouter
{
    public List<Rectangle> Rectangles { get; }
    public Result<Rectangle> PutNextRectangle(Size rectangleSize);
}