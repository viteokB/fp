using WordHandlers.Interfaces;

namespace WordHandlers.WordCounters;

public class FilteredWordsCounter : IWordCounter
{
    private readonly IEnumerable<Func<IEnumerable<string>, IEnumerable<string>>> filterStrategies;

    public FilteredWordsCounter(IEnumerable<Func<IEnumerable<string>, IEnumerable<string>>> filterStrategies)
    {
        this.filterStrategies = filterStrategies ?? throw new ArgumentNullException(nameof(filterStrategies));
    }

    public Dictionary<string, int> CountWords(IEnumerable<string> words)
    {
        if (words == null) throw new ArgumentNullException(nameof(words), "Input words cannot be null.");

        var filteredWords = filterStrategies
            .Aggregate(words, (words, filter) => filter(words));

        return filteredWords
            .Where(word => !string.IsNullOrWhiteSpace(word))
            .ToLookup(word => word, word => 1)
            .ToDictionary(group => group.Key, group => group.Count());
    }
}