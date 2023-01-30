namespace zugetextet.formulare.Services
{
    public interface ITokenService
    {
        string GenerateToken();
        bool ValidateCurrentToken(string token);
    }
}
