using System.Text;
using WordReaders.Settings;

namespace WordReaders.Readers;

public class FileReader(WordReaderSettings readerSettings) : IWordReader
{
    public readonly Encoding Encoding = readerSettings.Encoding;
    public readonly string FilePath = readerSettings.Path;

    public IEnumerable<string> Read()
    {
        var words = new List<string>();

        var lines = File.ReadAllLines(FilePath, Encoding)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        foreach (var line in lines)
        {
            if (line.Length > 1)
                throw new Exception("The file must contain no more than one word per line!");

            words.AddRange(line.Select(w => w.Trim()));
        }

        return words;
    }
}