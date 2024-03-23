namespace FullStack.API.DTOs
{
    public class SignInResponseDTO
    {

        public bool IsAuthSuccessfull { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public UserDTO userDTO { get; set; }
    }
}
