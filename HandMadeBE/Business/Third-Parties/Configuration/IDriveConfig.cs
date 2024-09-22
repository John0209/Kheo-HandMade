namespace ClassLibrary1.Third_Parties;

public interface IDriveConfig
{
    public string project_id { get; } 
    public string client_id { get; } 
    public string auth_uri { get; } 
    public string token_uri { get; } 
    public string auth_provider_x509_cert_url { get; } 
    public string client_secret { get; } 
    public string type { get; } 
    public string private_key_id { get; } 
     public string private_key { get; } 
     public string client_email { get; } 
     public string universe_domain { get; }
   
}