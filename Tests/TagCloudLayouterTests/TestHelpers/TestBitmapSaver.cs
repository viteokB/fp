using System.Drawing;

namespace Tests.TagCloudLayouterTests.TestHelpers;

public static class BitmapSaver
{
    private static readonly string projectDirectory = GetProjectDirectory();

    private static readonly string imagesDirectory = GetDirectoryFormProjectDir("CorrectImages");

    private static readonly string failureDirectory = GetDirectoryFormProjectDir("FailImages");

    public static void SaveToCorrect(Bitmap bitmap, string subDir, string fileName)
    {
        SaveBitmapTo(bitmap, Path.Combine(imagesDirectory, subDir), fileName);
    }

    public static void SaveToFail(Bitmap bitmap, string subDir, string fileName)
    {
        SaveBitmapTo(bitmap, Path.Combine(failureDirectory, subDir), fileName);
    }

    public static void SaveBitmapTo(Bitmap bitmap, string directory, string fileName)
    {
        if (bitmap == null)
            throw new ArgumentNullException(nameof(bitmap), "Given bitmap is null");

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be null or empty", nameof(fileName));

        if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            throw new DirectoryNotFoundException($"The directory '{directory}' does not exist.");

        try
        {
            bitmap.Save(Path.Combine(directory, fileName));
        }
        catch (IOException ex)
        {
            throw new IOException($"Failed to save bitmap to '{Path.Combine(directory, fileName)}'", ex);
        }
    }

    private static string GetProjectDirectory()
    {
        var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

        var projectDir = directoryInfo.Parent.Parent.Parent.FullName;

        return projectDir;
    }

    private static string GetDirectoryFormProjectDir(string getDirctory)
    {
        var resultDir = Path.Combine(projectDirectory, getDirctory);

        if (!Directory.Exists(resultDir))
            throw new DirectoryNotFoundException($"Directory {resultDir} not found");

        return resultDir;
    }
}