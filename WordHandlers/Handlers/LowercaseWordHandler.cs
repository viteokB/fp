namespace WordHandlers.Handlers;

public class LowercaseWordHandler : IWordHandler
{
    public IEnumerable<string> ApplyWordHandler(IEnumerable<string> words)
    {
        return words.Select(w => w.ToLower());
    }
}