using WordReaders.Readers;
using WordReaders.Settings;

namespace WordReaders.Factory;

public class WordReaderFactory : IWordReaderFactory
{
    public IWordReader CreateWordReader(WordReaderSettings settings)
    {
        return Path.GetExtension(settings.Path).ToLower() switch
        {
            ".txt" => new FileReader(settings),
            ".docx" => new DocxFileReader(settings),
            ".doc" => new DocFileReader(settings),
            _ => throw new ArgumentOutOfRangeException(nameof(settings.Path), settings.Path, "Unsupported file type")
        };
    }
}