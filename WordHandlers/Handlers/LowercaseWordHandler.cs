using FileSenderRailway;

namespace WordHandlers.Handlers;

public class LowercaseWordHandler : IWordHandler
{
    public Result<IEnumerable<string>> ApplyWordHandler(IEnumerable<string> words)
    {
        if (words == null)
            return Result.Fail<IEnumerable<string>>("Words argument cannot be null.");
        return words.Select(w => w.ToLower()).AsResult();
    }
}