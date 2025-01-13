using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud.GeometricPrimitives;

public class Triangle : IPrimitivePointGenerator
{
    public readonly Point Center;

    public readonly int Height;

    public Triangle(Point center, int height)
    {
        Center = center;
        Height = height;
    }

    public IEnumerable<Point> GetPoints()
    {
        var triangleSections = CreateTrianglePoints(Center, Height);

        foreach (var section in triangleSections)
        foreach (var sectionPoint in section.GetPoints())
            yield return sectionPoint;
    }

    private Section[] CreateTrianglePoints(Point center, int height)
    {
        return new[]
        {
            new Section(new Point(center.X + height, center.Y + height), new Point(center.X, center.Y - height), 1),
            new Section(new Point(center.X, center.Y - height), new Point(center.X - height, center.Y + height), 1),
            new Section(new Point(center.X + height, center.Y + height),
                new Point(center.X - height, center.Y + height), 1)
        };
    }
}