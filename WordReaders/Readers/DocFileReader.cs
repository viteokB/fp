using System.Text;
using FileSenderRailway;
using Spire.Doc;
using Spire.Doc.Documents;
using WordReaders.Settings;

namespace WordReaders.Readers;

public class DocFileReader : IWordReader
{
    public readonly Encoding Encoding;
    public readonly string FilePath;

    public DocFileReader(WordReaderSettings readerSettings)
    {
        if (readerSettings == null)
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
        return Result.Of(() => GetFileParagraphs())
            .Then(paragraphs => GetWordsFromParagraphs(paragraphs));
    }

    private IEnumerable<Paragraph> GetFileParagraphs()
    {
        var document = new Document();
        document.LoadFromFile(FilePath);

        foreach (Section section in document.Sections)
        foreach (Paragraph paragraph in section.Paragraphs)
            yield return paragraph;
    }

    private Result<List<string>> GetWordsFromParagraphs(IEnumerable<Paragraph> paragraphs)
    {
        var words = new List<string>();

        foreach (var paragraph in paragraphs)
        {
            var text = paragraph.Text.Trim();

            if (string.IsNullOrWhiteSpace(text))
                continue;

            var lineWords = text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (lineWords.Length > 1)
                return Result.Fail<List<string>>("The doc file must contain no more than one word per line!");

            words.Add(lineWords.First().Trim());
        }

        return words.AsResult();
    }
}