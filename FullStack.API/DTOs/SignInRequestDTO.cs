using System.ComponentModel.DataAnnotations;

namespace FullStack.API.DTOs
{
    public class SignInRequestDTO
    {
        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
