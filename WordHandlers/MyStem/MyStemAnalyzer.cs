using System.Diagnostics;
using System.Text;
using Newtonsoft.Json.Linq;
using WordHandlers.MyStem.InfoClasses;

namespace WordHandlers.MyStem;

public class MyStemAnalyzer : IDisposable
{
    private const string mystemPath = @"MyStem\mystem.exe";

    private const string inputFilePath = "input.txt";

    private static Process process;

    private static readonly HashSet<char> partSpeachSeparator = new() { ',', '=' };

    private bool disposed;

    static MyStemAnalyzer()
    {
        StartMyStemProcess();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private static void StartMyStemProcess()
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = mystemPath,
            Arguments = $"-nig -e utf-8 --eng-gr --format json {inputFilePath}", // Используем input.txt
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8
        };
        process = Process.Start(startInfo);
    }

    public IEnumerable<WordInfo> AnalyzeWords(IEnumerable<string> words)
    {
        // Запись слов в файл input.txt
        File.WriteAllLines(inputFilePath, words);

        if (process == null || process.HasExited)
            StartMyStemProcess();

        // Чтение результата из стандартного вывода
        var jsonResult = process.StandardOutput.ReadToEnd();

        // Разделяем вывод на отдельные строки (каждая строка - отдельный JSON объект)
        var results = jsonResult.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(ParseWordInfos);

        return results;
    }

    private static WordInfo ParseWordInfos(string jsonLine)
    {
        var item = JObject.Parse(jsonLine);
        var text = item["text"]?.ToString() ?? "Неизвестно";

        var analyses = item["analysis"]?.ToObject<List<Analysis>>() ?? new List<Analysis>();
        var partOfSpeach = DefinePartOfSpeech(analyses[0].gr);
        var lex = analyses[0].lex;
        var wordInfos = new WordInfo(text, partOfSpeach, lex);

        return wordInfos;
    }

    private static PartOfSpeech DefinePartOfSpeech(string gr)
    {
        var partSpeechBuilder = new StringBuilder(6);
        foreach (var symbol in gr)
        {
            if (partSpeachSeparator.Contains(symbol))
                break;

            partSpeechBuilder.Append(symbol);
        }

        var partOfSpeechStr = partSpeechBuilder.ToString();

        if (Enum.TryParse(partOfSpeechStr, out PartOfSpeech partOfSpeech))
            return partOfSpeech;

        return PartOfSpeech.UNKNOWN;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (process != null && !process.HasExited)
            {
                process.CloseMainWindow();
                process.WaitForExit();
                process.Dispose();
            }

            disposed = true;
        }
    }

    ~MyStemAnalyzer()
    {
        Dispose(false);
    }
}