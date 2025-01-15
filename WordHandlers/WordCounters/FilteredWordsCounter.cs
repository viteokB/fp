using FileSenderRailway;
using WordHandlers.Interfaces;

namespace WordHandlers.WordCounters;

public class FilteredWordsCounter : IWordCounter
{
    private readonly IEnumerable<Func<IEnumerable<string>, Result<IEnumerable<string>>>> filterStrategies;

    public FilteredWordsCounter(IEnumerable<Func<IEnumerable<string>, Result<IEnumerable<string>>>> filterStrategies)
    {
        this.filterStrategies = filterStrategies ?? throw new ArgumentNullException(nameof(filterStrategies));
    }

    public Result<Dictionary<string, int>> CountWords(IEnumerable<string> words)
    {
        if (words == null)
            Result.Fail<Dictionary<string, int>>("Input words cannot be null.");

        return Result.Of(() => GetFilteredWords(words))
            .Then(GetDictionaryWordsCounts);
    }

    private IEnumerable<string> GetFilteredWords(IEnumerable<string> words)
    {
        return filterStrategies
            .Aggregate(words, (words, filter) => filter(words).GetValueOrThrow());
    }

    private Dictionary<string, int> GetDictionaryWordsCounts(IEnumerable<string> words)
    {
        return words
            .Where(word => !string.IsNullOrWhiteSpace(word))
            .ToLookup(word => word, word => 1)
            .ToDictionary(group => group.Key, group => group.Count());
    }
}