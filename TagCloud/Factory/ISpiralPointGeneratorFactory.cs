using TagCloud.Interfaces;
using TagCloud.Visualisers;

namespace TagCloud.Factory;

public interface ISpiralPointGeneratorFactory
{
    ISpiralPointGenerator CreateSpiralPointGenerator(ImageCreateSettings imageSettings);
}