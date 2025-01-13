using System.Drawing;
using System.Drawing.Imaging;

namespace BitmapSavers;

public class BitmapSaver
{
    public void Save(ImageSaveSettings saveSettings, Bitmap bitmap)
    {
        var currentDir = Directory.GetCurrentDirectory();
        var fullPath = currentDir + saveSettings.Path;

        if (bitmap == null) throw new ArgumentNullException(nameof(bitmap), "Bitmap cannot be null.");
        if (string.IsNullOrEmpty(fullPath))
            throw new ArgumentException("Path cannot be null or empty.", nameof(fullPath));

        var extension = Path.GetExtension(fullPath).ToLower();
        var imageFormat = GetImageFormat(extension);

        try
        {
            bitmap.Save(fullPath, imageFormat);
        }
        catch (Exception ex)
        {
            throw new IOException($"Error saving image to path: {fullPath}. Error: {ex.Message}", ex);
        }
    }

    private ImageFormat GetImageFormat(string extension)
    {
        return extension switch
        {
            ".bmp" => ImageFormat.Bmp,
            ".emf" => ImageFormat.Emf,
            ".jpeg" => ImageFormat.Jpeg,
            ".png" => ImageFormat.Png,
            ".wmf" => ImageFormat.Wmf,
            _ => throw new ArgumentException("The transmitted type is not supported")
        };
    }
}