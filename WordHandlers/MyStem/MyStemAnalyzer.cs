using System.Diagnostics;
using System.Text;
using FakeItEasy.Configuration;
using FileSenderRailway;
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

    public Result<IEnumerable<WordInfo>> AnalyzeWords(IEnumerable<string> words)
    {
        return WriteWordsToInputFile(words)
            .Then(_ => StartMyStemProcess())
            .Then(_ => ReadAndParseOutput());
    }

    private Result<None> WriteWordsToInputFile(IEnumerable<string> words)
    {
        return Result.OfAction(() => File.WriteAllLines(inputFilePath, words));
    }

    private static Result<None> StartMyStemProcess()
    {
        if (!File.Exists(mystemPath))
            return Result.Fail<None>($"mystem.exe not found on path '{mystemPath}'");

        var startInfo = new ProcessStartInfo
        {
            FileName = mystemPath,
            Arguments = $"-nig -e utf-8 --eng-gr --format json {inputFilePath}", // Используем input.txt
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8
        };
        var startProcess = Result.OfAction(() => process = Process.Start(startInfo));

        if (!startProcess.IsSuccess || process == null)
            return startProcess.RefineError("Process startup error, process mystem.exe was not started");

        return startProcess;
    }

    private Result<IEnumerable<WordInfo>> ReadAndParseOutput()
    {
        if (process == null || process.HasExited)
            return Result.Fail<IEnumerable<WordInfo>>("MyStem process is not running or has exited.");

        return Result.Of(() =>
        {
            string jsonResult = process.StandardOutput.ReadToEnd();
            return jsonResult.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(winfo => ParseWordInfo(winfo).GetValueOrThrow());
        }).RefineError("Error reading output");
    }


    private static Result<WordInfo> ParseWordInfo(string jsonLine)
    {
        var item = JObject.Parse(jsonLine);
        var text = item["text"]?.ToString();
        var analyses = item["analysis"]?.ToObject<List<Analysis>>();

        if (text == null || analyses == null)
            return Result.Fail<WordInfo>(
                $"Error parsing reading JSON line. NULL value of properties: " +
                $"text = {text == null}, analyses = {analyses == null}");

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

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~MyStemAnalyzer()
    {
        Dispose(false);
    }
}