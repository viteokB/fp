using WordReaders.Factory;
using WordReaders.Interfaces;
using WordReaders.Settings;

namespace WordReaders.Readers;

public class MultiFormatWordReader : IMultiFormatReader
{
    private readonly IWordReaderFactory factory;

    public MultiFormatWordReader(IWordReaderFactory wordReaderFactory)
    {
        factory = wordReaderFactory;
    }

    public IEnumerable<string> Read(WordReaderSettings settings)
    {
        var wordReader = factory.CreateWordReader(settings);
        return wordReader.Read();
    }
}