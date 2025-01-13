using WordHandlers.MyStem;
using WordHandlers.MyStem.InfoClasses;

namespace WordHandlers.Handlers;

public class BoringWordHandler : IWordHandler, IDisposable
{
    private static readonly MyStemAnalyzer myStemAnalyzer = new();

    //В принципе лего можно добавить настройку пользователем(мне не интересно)
    private static readonly HashSet<PartOfSpeech> notBoringPartOfSpeeches =
        NotBoringConfiguration.NotBoringPartOfSpeeches;

    private bool disposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IEnumerable<string> ApplyWordHandler(IEnumerable<string> words)
    {
        if (words == null) return Enumerable.Empty<string>();

        return myStemAnalyzer
            .AnalyzeWords(words)
            .Where(IsNotBoringWord)
            .Select(wordInfo => wordInfo.Lemma);
    }

    private static bool IsNotBoringWord(WordInfo wordInfo)
    {
        return notBoringPartOfSpeeches.Contains(wordInfo.PartOfSpeech);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
            if (disposing)
                // Освобождаем управляемые ресурсы
                myStemAnalyzer.Dispose();
        disposed = true;
    }

    ~BoringWordHandler()
    {
        Dispose(false);
    }
}