using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud;

public class TagCloudLayouter : ICloudLayouter
{
    private readonly ISpiralPointGenerator PointGenerator;

    public TagCloudLayouter(ISpiralPointGenerator rayMover)
    {
        PointGenerator = rayMover;
    }

    public List<Rectangle> Rectangles { get; } = new();

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
            throw new ArgumentException("The height and width of the Rectangle must be greater than 0");

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

        throw new InvalidOperationException("No suitable location found for the rectangle.");
    }
}