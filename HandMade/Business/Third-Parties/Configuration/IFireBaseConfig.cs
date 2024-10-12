namespace Kitchen.Application.Gateway.IConfiguration;


public interface IFireBaseConfig
{
    public string ApiKey { get; } 
    public string Bucket { get; }
    public string AuthEmail { get; }
    public string AuthPassword { get; }
}
