using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud.GeometricPrimitives;

public struct Section : IPrimitivePointGenerator
{
    public Point StartPoint;

    public Point EndPoint;

    public double MovePointStep;

    public Section(Point startPoint, Point endPoint, double movePointStep)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
        MovePointStep = movePointStep;
    }

    public IEnumerable<Point> GetPoints()
    {
        if (StartPoint == EndPoint)
        {
            yield return StartPoint; // Возвращаем единственную точку
            yield break; // Завершаем выполнение метода
        }

        double dx = EndPoint.X - StartPoint.X;
        double dy = EndPoint.Y - StartPoint.Y;

        // Длина отрезка
        var length = Math.Sqrt(dx * dx + dy * dy);

        // Количество шагов, необходимых для достижения конца отрезка
        var steps = (int)(length / MovePointStep);

        for (var i = 0; i <= steps; i++)
        {
            // Вычисляем текущую точку
            var t = (double)i / steps; // Параметр от 0 до 1
            var x = (int)(StartPoint.X + t * dx);
            var y = (int)(StartPoint.Y + t * dy);
            yield return new Point(x, y);
        }
    }
}