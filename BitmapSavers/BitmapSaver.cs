using System.Drawing;
using System.Drawing.Imaging;
using FileSenderRailway;

namespace BitmapSavers;

public class BitmapSaver
{
    public Result<None> Save(ImageSaveSettings saveSettings, Bitmap bitmap)
    {
        var validationResult = ValidateInputs(saveSettings, bitmap);
        if (!validationResult.IsSuccess)
            return validationResult;

        var savePathResult = GetImageSavePath(saveSettings);

        //Тут проверится savePathResult, на IsSuccess
        var imageFormatResult = savePathResult
            .Then(path => GetImageFormatResult(path));

        return imageFormatResult
                .Then((imageFormat) => bitmap.Save(
                    savePathResult.GetValueOrThrow(),
                    imageFormat));
    }

    private Result<None> ValidateInputs(ImageSaveSettings saveSettings, Bitmap bitmap)
    {
        if (bitmap == null)
            return Result.Fail<None>("Bitmap cannot be null.");
        if (saveSettings == null)
            return Result.Fail<None>("ImageSaveSettings cannot be null.");

        return Result.Ok();
    }

    private Result<string> GetImageSavePath(ImageSaveSettings saveSettings)
    {
        if (string.IsNullOrEmpty(saveSettings.Path))
            return Result.Fail<string>("ImageSaveSettings.Path cannot be null or empty.");

        return Result.Of(() => Directory.GetCurrentDirectory())
            .Then(currentDir => Path.Combine(currentDir, saveSettings.Path));
    }

    private Result<ImageFormat> GetImageFormatResult(Result<string> fullPathResult)
    {
        return fullPathResult
            .Then(path => Path.GetExtension(path).ToLower())
            .Then(result => GetImageFormat(result));
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