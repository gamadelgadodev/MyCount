namespace API.DTOs
{
    public class NewUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
    public class UpdateUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
    public class LoginUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class UserDTO
    {
    }
}
