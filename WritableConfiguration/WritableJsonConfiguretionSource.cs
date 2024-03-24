using Microsoft.Extensions.Configuration.Json;

namespace WritableOptions;

public class WritableJsonConfiguretionSource: JsonConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        EnsureDefaults(builder);
        return new WritableJsonConfigurationProvider(this);
    }
}