using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud.RayMovers;

public class CircularSpiralPointCreator : ISpiralPointGenerator
{
    private const double OneRound = Math.PI * 2;

    //В радианах
    private readonly double angleStep;

    private readonly double radiusStep;
    private readonly Ray spiralRay;

    public CircularSpiralPointCreator(Point center, double radiusStep = 1, double angleStep = 5,
        int startRadius = 0, int startAngle = 0)
    {
        if (center.X <= 0 || center.Y <= 0)
            throw new ArgumentException("SpiralRayMover center Point should have positive X and Y");

        if (radiusStep <= 0 || angleStep <= 0)
            throw new ArgumentException("radiusStep and angleStep should be positive");

        spiralRay = new Ray(center, startRadius, startAngle);
        this.radiusStep = radiusStep;

        //Преобразование из градусов в радианы
        this.angleStep = angleStep * Math.PI / 180;
    }

    public IEnumerable<Point> GeneratePoints()
    {
        while (true)
        {
            yield return spiralRay.EndPoint;

            //Радиус увеличивается на 1 только после полного прохождения круга
            spiralRay.Update(radiusStep / OneRound * angleStep, angleStep);
        }
    }
}