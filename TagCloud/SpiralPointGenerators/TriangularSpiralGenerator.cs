using System.Drawing;
using TagCloud.GeometricPrimitives;
using TagCloud.Interfaces;

namespace TagCloud.SpiralPointGenerators;

public class TriangularSpiralPointCreator : ISpiralPointGenerator
{
    private readonly Point Center;

    private readonly int Step;

    private int CurrentHeight;

    public TriangularSpiralPointCreator(Point center, int step = 1)
    {
        Center = center;
        Step = step;
    }

    public IEnumerable<Point> GeneratePoints()
    {
        while (true)
        {
            var triangle = new Triangle(Center, CurrentHeight);
            foreach (var point in triangle.GetPoints()) yield return point;

            CurrentHeight += Step;
        }
    }
}