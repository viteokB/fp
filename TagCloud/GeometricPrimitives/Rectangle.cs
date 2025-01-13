using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud.GeometricPrimitives;

public class Rectangle : IPrimitivePointGenerator
{
    public readonly Point Center;
    public readonly int Height;
    public readonly int Width;

    public Rectangle(Point center, int width, int height)
    {
        Center = center;
        Width = width;
        Height = height;
    }

    public IEnumerable<Point> GetPoints()
    {
        var rectangleSections = CreateRectanglePoints(Center, Width, Height);

        foreach (var section in rectangleSections)
        foreach (var sectionPoint in section.GetPoints())
            yield return sectionPoint;
    }

    private Section[] CreateRectanglePoints(Point center, int width, int height)
    {
        // Координаты углов прямоугольника
        var topLeft = new Point(center.X - width / 2, center.Y + height / 2);
        var topRight = new Point(center.X + width / 2, center.Y + height / 2);
        var bottomLeft = new Point(center.X - width / 2, center.Y - height / 2);
        var bottomRight = new Point(center.X + width / 2, center.Y - height / 2);

        return new[]
        {
            new Section(topLeft, topRight, 1),
            new Section(topRight, bottomRight, 1),
            new Section(bottomRight, bottomLeft, 1),
            new Section(bottomLeft, topLeft, 1)
        };
    }
}