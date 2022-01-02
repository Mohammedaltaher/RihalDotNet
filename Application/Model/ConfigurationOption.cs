namespace Application.Model;
public class ConnectionStringsOptions
{
    public const string ConnectionStrings = "ConnectionStrings";

    public string DefaultConnection { get; set; }

}
public class JWTOptions
{
    public const string JWT = "JWT";
    public string Secret { get; set; }
    public string Key { get; set; }
    public string Issuer { get; set; }
}
