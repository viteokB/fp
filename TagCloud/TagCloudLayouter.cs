using System.Drawing;
using FileSenderRailway;
using TagCloud.Interfaces;

namespace TagCloud;

public class TagCloudLayouter : ICloudLayouter
{
    public readonly ISpiralPointGenerator PointGenerator;

    public TagCloudLayouter(ISpiralPointGenerator rayMover)
    {
        PointGenerator = rayMover;
    }

    public List<Rectangle> Rectangles { get; } = new();

    public Result<Rectangle> PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
            return Result.Fail<Rectangle>("The height and width of the Rectangle must be greater than 0");

        foreach (var point in PointGenerator.GeneratePoints())
        {
            var location = new Point(point.X - rectangleSize.Width / 2,
                point.Y - rectangleSize.Height / 2);

            var rectangle = new Rectangle(location, rectangleSize);

            // Проверяем, пересекается ли новый прямоугольник с уже существующими
            if (!Rectangles.Any(r => r.IntersectsWith(rectangle)))
            {
                Rectangles.Add(rectangle);
                return rectangle;
            }
        }

        return Result.Fail<Rectangle>("No suitable location found for the rectangle.");
    }
}