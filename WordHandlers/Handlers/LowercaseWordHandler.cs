using FileSenderRailway;

namespace WordHandlers.Handlers;

public class LowercaseWordHandler : IWordHandler
{
    public Result<IEnumerable<string>> ApplyWordHandler(IEnumerable<string> words)
    {
        return words.Select(w => w.ToLower()).AsResult();
    }
}