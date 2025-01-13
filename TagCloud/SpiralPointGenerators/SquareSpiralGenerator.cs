using System.Drawing;
using TagCloud.Interfaces;
using Rectangle = TagCloud.GeometricPrimitives.Rectangle;

namespace TagCloud.SpiralPointGenerators;

public class SquareSpiralPointCreator : ISpiralPointGenerator
{
    private readonly double AspectRatio; // Соотношение сторон
    private readonly Point Center;
    private readonly int Step; // Шаг увеличения размеров прямоугольника
    private int CurrentHeight; // Текущая высота прямоугольника
    private int CurrentWidth; // Текущая ширина прямоугольника

    public SquareSpiralPointCreator(Point center, int initialWidth = 2, int initialHeight = 2, int step = 1)
    {
        Center = center;
        CurrentWidth = initialWidth;
        CurrentHeight = initialHeight;
        Step = step;
        AspectRatio = (double)initialWidth / initialHeight;
    }

    public IEnumerable<Point> GeneratePoints()
    {
        while (true)
        {
            var rectangle = new Rectangle(Center, CurrentWidth, CurrentHeight);
            foreach (var point in rectangle.GetPoints()) yield return point;

            //Потому как потом мы берем половину ширины для увеличения координаты
            //И если она равна не целом числу мы просто произведем в 2 раза больше итераций
            CurrentWidth += 2 * Step;
            CurrentHeight = (int)(CurrentWidth / AspectRatio);
        }
    }
}