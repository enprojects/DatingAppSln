using DatingApp.API.UserDto;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Validators
{
    public class ValidateUserAttribute : ValidationAttribute
    {
      

        public ValidateUserAttribute( )
        {
             
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = ((UserRegistrationDto)validationContext.ObjectInstance);


            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
