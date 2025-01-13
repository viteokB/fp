using System.Drawing;

namespace TagCloud.Visualisers;

public record TagCloudWord(Rectangle Box, string TextWord, int FontSize);