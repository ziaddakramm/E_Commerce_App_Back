using e_commerce_api.Controllers;

namespace e_commerce_api
{
    public class AuthenticateResponse
    {
       /*  public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }*/
        public string Username { get; set; }

         public string Password { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(UserCredential userCredential,string token)
        {
            Username=userCredential.UserName;
            Password=userCredential.Password;
            Token = token;
        }
    }
}