namespace FullStack.API.DTOs
{
    public class SignUpResponseDTO
    {
        public bool IsRegistrationSuccessfull { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
