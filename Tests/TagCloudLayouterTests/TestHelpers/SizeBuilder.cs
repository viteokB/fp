using System.Drawing;

namespace TestHelpers.TagCloudLayouterTests.Helpers;

public class SizeBuilder : ICountConfigurator, IWidthConfigurator, IHeightConfigurator, IGenerator<Size>
{
    private int _maxHeight;

    private int _maxWidth;

    private int _minHeight;

    private int _minWidth;
    private int generateRectangleCount;

    public IWidthConfigurator SetCount(int count)
    {
        if (count <= 0)
            throw new ArgumentException("Count should be positive");

        generateRectangleCount = count;
        return this;
    }

    public IEnumerable<Size> Generate()
    {
        var random = new Random();

        for (var i = 0; i < generateRectangleCount; i++)
        {
            // Генерация случайной ширины и высоты в заданных пределах
            var width = random.Next(_minWidth, _maxWidth + 1);
            var height = random.Next(_minHeight, _maxHeight + 1);

            yield return new Size(width, height);
        }
    }

    public IGenerator<Size> SetHeight(int minHeight, int maxHeight)
    {
        if (minHeight <= 0 || maxHeight <= 0)
            throw new ArgumentException("Arguments should be positive");

        _minHeight = minHeight;
        _maxHeight = maxHeight;

        return this;
    }

    public IHeightConfigurator SetWidth(int minWidth, int maxWidth)
    {
        if (minWidth <= 0 || maxWidth <= 0)
            throw new ArgumentException("Arguments should be positive");

        _minWidth = minWidth;
        _maxWidth = maxWidth;

        return this;
    }

    public static ICountConfigurator Configure()
    {
        return new SizeBuilder();
    }
}

public interface ICountConfigurator
{
    public IWidthConfigurator SetCount(int count);
}

public interface IWidthConfigurator
{
    public IHeightConfigurator SetWidth(int minWidth, int maxWidth);
}

public interface IHeightConfigurator
{
    public IGenerator<Size> SetHeight(int minHeight, int maxHeight);
}

public interface IGenerator<T>
{
    public IEnumerable<T> Generate();
}