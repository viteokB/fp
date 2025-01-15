using ConsoleClient.Services;
using FileSenderRailway;

namespace ConsoleClient.Interfaces;

public interface ISettingsProvider
{
    public Result<SettingsStorage> GetSettingsStorage();
}