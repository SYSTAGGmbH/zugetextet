using zugetextet.formulare.Data.Models;
using zugetextet.formulare.DTOs;

namespace zugetextet.formulare.Services
{
    public interface ILoginDataService
    {
        Task<LoginDataDto> CreateLoginData(LoginDataDto loginDataDto);
        Task<List<LoginDataDto>> GetAllLoginData();
        LoginData GetLoginData(string username);
        string CreateSHA512(string strData);


    }
}
