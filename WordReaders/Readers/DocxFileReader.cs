using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using WordReaders.Settings;

namespace WordReaders.Readers;

public class DocxFileReader : IWordReader
{
    public readonly string FilePath;

    public DocxFileReader(WordReaderSettings readerSettings)
    {
        FilePath = readerSettings.Path ??
                   throw new ArgumentNullException(nameof(readerSettings.Path), "File path cannot be null.");

        if (!File.Exists(FilePath)) throw new FileNotFoundException($"File not found: {FilePath}");
    }

    public IEnumerable<string> Read()
    {
        var words = new List<string>();

        using (var wordDoc = WordprocessingDocument.Open(FilePath, false))
        {
            var body = wordDoc.MainDocumentPart.Document.Body;

            foreach (var paragraph in body.Elements<Paragraph>())
            {
                var text = paragraph.InnerText.Trim();

                // Пропускаем пустые параграфы
                if (string.IsNullOrWhiteSpace(text))
                    continue;

                // Разделяем текст на слова
                var lineWords = text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                // Проверяем, что в параграфе не более одного слова
                if (lineWords.Length > 1)
                    throw new Exception("The docx file must contain no more than one word per line!");

                // Добавляем слово в список, если оно не пустое
                words.Add(lineWords.First().Trim());
            }
        }

        return words;
    }
}