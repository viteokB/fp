using System.Text;

namespace WordReaders.Settings;

public record WordReaderSettings(string Path, Encoding Encoding);