namespace e_commerce_api
{
    public interface IJwtAuth
    {
            public string Authentication(string username, string password);
    }
}