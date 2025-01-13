using FileSenderRailway;
using WordReaders.Settings;

namespace WordReaders.Interfaces
{
    public interface IMultiFormatReader
    {
        public Result<List<string>> Read(WordReaderSettings settings);
    }
}
