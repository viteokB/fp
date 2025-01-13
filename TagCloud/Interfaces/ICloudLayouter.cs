using System.Drawing;

namespace TagCloud.Interfaces;

public interface ICloudLayouter
{
    public List<Rectangle> Rectangles { get; }
    public Rectangle PutNextRectangle(Size rectangleSize);
}