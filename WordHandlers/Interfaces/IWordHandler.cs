namespace WordHandlers;

public interface IWordHandler
{
    public IEnumerable<string> ApplyWordHandler(IEnumerable<string> words);
}