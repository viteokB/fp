using FileSenderRailway;

namespace WordHandlers;

public interface IWordHandler
{
    public Result<IEnumerable<string>> ApplyWordHandler(IEnumerable<string> words);
}