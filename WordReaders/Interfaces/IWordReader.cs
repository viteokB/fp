namespace WordReaders;

public interface IWordReader
{
    public IEnumerable<string> Read();
}