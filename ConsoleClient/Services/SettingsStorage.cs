using BitmapSavers;
using TagCloud.Visualisers;
using WordReaders.Settings;

namespace ConsoleClient.Services;

public record SettingsStorage(
    ImageCreateSettings ImageCreate,
    ImageSaveSettings ImageSave,
    WordReaderSettings ReaderSettings);