using System.Text.Json;

namespace WritableOptions;

public class ConfigurationsService<T>(IConfiguration configuration)
    where T : class, new()
{
    private readonly IConfigurationRoot _configurationRoot = (configuration as IConfigurationRoot)!;

    public T? GetFileConfiguration(string section)
    {
        var config = _configurationRoot.GetSection(section).Value;
        return (string.IsNullOrEmpty(config)) ? null : JsonSerializer.Deserialize<T>(config);
    }

    public void SetFileConfiguration(string section, T value)
    {
        _configurationRoot.GetSection(section).Value = JsonSerializer.Serialize(value);
        _configurationRoot.Reload();
    }
}