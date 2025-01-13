namespace WordHandlers.Interfaces;

public interface IWordCounter
{
    public Dictionary<string, int> CountWords(IEnumerable<string> words);
}