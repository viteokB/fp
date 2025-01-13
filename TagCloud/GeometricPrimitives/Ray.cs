using System.Drawing;

namespace TagCloud;

public class Ray
{
    public readonly Point StartPoint;

    public Ray(Point startPoint, int radius, int angle)
    {
        StartPoint = startPoint;

        Radius = radius;

        Angle = angle;
    }

    public double Radius { get; private set; }

    //В радианах
    public double Angle { get; private set; }

    public Point EndPoint => new()
    {
        X = StartPoint.X + (int)(Radius * Math.Cos(Angle)),
        Y = StartPoint.Y + (int)(Radius * Math.Sin(Angle))
    };

    public void Update(double deltaRadius, double deltaAngle)
    {
        Radius += deltaRadius;
        Angle += deltaAngle;
    }
}