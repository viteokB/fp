using FileSenderRailway;

namespace WordReaders;

public interface IWordReader
{
    public Result<List<string>> Read();
}