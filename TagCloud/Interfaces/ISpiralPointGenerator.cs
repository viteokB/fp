using System.Drawing;

namespace TagCloud.Interfaces;

public interface ISpiralPointGenerator
{
    IEnumerable<Point> GeneratePoints();
}