using Microsoft.Extensions.Configuration;

namespace ClassLibrary1.Third_Parties;

public class CloudConfig:ICloudConfig
{
    private readonly IConfigurationSection _section;
    public static string ConfigName =>"Cloud";

    public string project_id => _section[nameof(project_id)];
    public string client_id => _section[nameof(client_id)];
    public string auth_uri => _section[nameof(auth_uri)];
    public string token_uri => _section[nameof(token_uri)];
    public string auth_provider_x509_cert_url => _section[nameof(auth_provider_x509_cert_url)];
    public string client_secret => _section[nameof(client_secret)];
    public string type => _section[nameof(type)];
    public string private_key_id => _section[nameof(private_key_id)];
    public string private_key => _section[nameof(private_key)];
    public string client_email => _section[nameof(client_email)];
    public string universe_domain => _section[nameof(universe_domain)];

    public CloudConfig(IConfiguration configuration)
    {
        _section = configuration.GetSection(ConfigName);
    }
}