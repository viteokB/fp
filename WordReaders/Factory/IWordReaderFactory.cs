using WordReaders.Settings;

namespace WordReaders.Factory;

public interface IWordReaderFactory
{
    public IWordReader CreateWordReader(WordReaderSettings settings);
}