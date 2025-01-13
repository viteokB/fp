using System.Drawing;

namespace TagCloud.Interfaces;

public interface IPrimitivePointGenerator
{
    public IEnumerable<Point> GetPoints();
}