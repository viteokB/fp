using System.Drawing;

namespace TestHelpers.TagCloudLayouterTests.Helpers.RectangleOnlyVisualiser;

public interface IRectangleVisualiser : IDisposable
{
    public void DrawRectangle(Bitmap bitmap, Rectangle rectangle);
}