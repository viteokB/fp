using WordReaders.Settings;

namespace WordReaders.Interfaces
{
    public interface IMultiFormatReader
    {
        public IEnumerable<string> Read(WordReaderSettings settings);
    }
}
