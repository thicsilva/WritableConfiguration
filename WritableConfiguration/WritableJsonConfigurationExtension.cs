using Microsoft.Extensions.FileProviders;

namespace WritableOptions;

public static class WritableJsonConfigurationExtension
{
    public static IConfigurationBuilder AddWritableJsonFile(this IConfigurationBuilder builder, string path)
    {
        return AddWritableJsonFile(builder, provider: null, path: path, optional: false, reloadOnChange: false);
    }
    
    public static IConfigurationBuilder AddWritableJsonFile(this IConfigurationBuilder builder, string path, bool optional)
    {
        return AddWritableJsonFile(builder, provider: null, path: path, optional: optional, reloadOnChange: false);
    }
    
    public static IConfigurationBuilder AddWritableJsonFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
    {
        return AddWritableJsonFile(builder, provider: null, path: path, optional: optional, reloadOnChange: reloadOnChange);
    }
    
    public static IConfigurationBuilder AddWritableJsonFile(this IConfigurationBuilder builder, IFileProvider? provider, string path, bool optional, bool reloadOnChange)
    {
        ArgumentNullException.ThrowIfNull(builder);

        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("Invalid file path", nameof(path));
        }

        return builder.AddWritableJsonFile(s =>
        {
            s.FileProvider = provider;
            s.Path = path;
            s.Optional = optional;
            s.ReloadOnChange = reloadOnChange;
            s.ResolveFileProvider();
        });
    }
    
    public static IConfigurationBuilder AddWritableJsonFile(this IConfigurationBuilder builder, Action<WritableJsonConfiguretionSource>? configureSource)
        => builder.Add(configureSource);
}