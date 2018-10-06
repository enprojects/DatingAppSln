
using DatingApp.API.Validators;

namespace DatingApp.API.UserDto
{
    [ValidateUserAttribute(ErrorMessage ="Invalid error message")]
    public class UserRegistrationDto

    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
