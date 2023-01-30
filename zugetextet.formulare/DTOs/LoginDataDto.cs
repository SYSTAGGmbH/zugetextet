namespace zugetextet.formulare.DTOs
{
    public class LoginDataDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
