using System.Drawing;

namespace TestHelpers.TagCloudLayouterTests.Helpers.RectangleOnlyVisualiser;

public class RectangleVisualiser : IRectangleVisualiser
{
    private readonly Pen pen;

    private bool isDisposed;

    public RectangleVisualiser(Color penColor, int penWidth)
    {
        pen = new Pen(penColor, penWidth);
    }

    public void DrawRectangle(Bitmap bitmap, Rectangle rectangle)
    {
        if (bitmap == null)
            throw new ArgumentNullException($"bitmap field cannot be null {bitmap}");

        using (var graphics = Graphics.FromImage(bitmap))
        {
            graphics.DrawRectangle(pen, rectangle);
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~RectangleVisualiser()
    {
        Dispose(false);
    }

    private void Dispose(bool fromMethod)
    {
        if (isDisposed)
            return;

        if (fromMethod) pen.Dispose();

        isDisposed = true;
    }
}