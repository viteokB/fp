using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using FileSenderRailway;
using WordReaders.Settings;

namespace WordReaders.Readers;

public class FileReader : IWordReader
{
    public readonly Encoding Encoding;

    public readonly string FilePath;

    public FileReader(WordReaderSettings readerSettings)
    {
        if(readerSettings == null)
            throw new ArgumentNullException(nameof(readerSettings), "Reader settings cannot be null.");
        FilePath = readerSettings.Path ??
                   throw new ArgumentNullException(nameof(readerSettings.Path), "File path cannot be null.");
        Encoding = readerSettings.Encoding ?? 
                   throw new ArgumentNullException(nameof(readerSettings.Encoding), "Encoding cannot be null.");
        if (!File.Exists(FilePath))
            throw new FileNotFoundException($"File not found: {FilePath}");
    }

    public Result<List<string>> Read()
    {
        return Result.Of(() => ReadLinesFromFile())
            .Then(lines => GetWordsFromLines(lines));
    }

    private IEnumerable<string[]> ReadLinesFromFile()
    {
        return File.ReadAllLines(FilePath, Encoding)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries));
    }

    private Result<List<string>> GetWordsFromLines(IEnumerable<string[]> lines)
    {
        var words = new List<string>();

        foreach (var line in lines)
        {
            if (line.Length > 1)
                return Result.Fail<List<string>>("The file must contain no more than one word per line!");

            words.AddRange(line.Select(w => w.Trim()));
        }

        return words.AsResult();
    }
}