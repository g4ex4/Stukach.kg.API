using System.ComponentModel.DataAnnotations;


namespace Application.Users.UserCommands.UserRegistration
{
    public class UserRequest : IRequest
    {
        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPasseord { get; set; }
    }
}
