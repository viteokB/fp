using FileSenderRailway;

namespace WordHandlers.Interfaces;

public interface IWordCounter
{
    public Result<Dictionary<string, int>> CountWords(IEnumerable<string> words);
}