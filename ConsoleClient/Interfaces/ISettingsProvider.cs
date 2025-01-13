using ConsoleClient.Services;

namespace ConsoleClient.Interfaces;

public interface ISettingsProvider
{
    public SettingsStorage GetSettingsStorage();
}