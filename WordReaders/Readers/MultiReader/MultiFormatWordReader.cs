using FileSenderRailway;
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

    public Result<List<string>> Read(WordReaderSettings settings)
    {
        return Result.Of(() => factory.CreateWordReader(settings))
            .Then(wordReader => wordReader.Read());
    }
}