using Kitchen.Application.Gateway.IConfiguration;
using Microsoft.Extensions.Configuration;

namespace ClassLibrary1.Third_Parties.Configuration;

public class FireBaseConfig : IFireBaseConfig
{
    private readonly IConfigurationSection _section;
    public static string ConfigName => "Firebase";

    public FireBaseConfig(Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        _section = configuration.GetSection(ConfigName);
    }
    public string ApiKey => _section[nameof(ApiKey)];
    public string Bucket => _section[nameof(Bucket)];
    public string AuthEmail => _section[nameof(AuthEmail)];
    public string AuthPassword => _section[nameof(AuthPassword)];
}