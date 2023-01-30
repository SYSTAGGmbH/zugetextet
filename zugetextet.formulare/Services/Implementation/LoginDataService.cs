using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using zugetextet.formulare.Data;
using zugetextet.formulare.Data.Models;
using zugetextet.formulare.DTOs;

namespace zugetextet.formulare.Services.Implementation
{
    public class LoginDataService : ILoginDataService
    {
        private readonly ApplicationDbContext _context;

        public LoginDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public LoginData GetLoginData(string username)
        {
            return _context.LoginData.Single(user => user.Username == username);
        }

        public async Task<List<LoginDataDto>> GetAllLoginData()
        {
            var loginDataList = await _context.LoginData
                .ToListAsync();
            List<LoginDataDto> loginDataDtoList = new();

            foreach (LoginData loginData in loginDataList)
            {
                loginDataDtoList.Add(new LoginDataDto()
                {
                    Id = loginData.Id,
                    Username = loginData.Username,
                    Password = loginData.Password,

                });
            }

            return loginDataDtoList;
        }

        public async Task<LoginDataDto> CreateLoginData(LoginDataDto loginDataDto)
        {
            var loginData = new LoginData()
            {
                Id = Guid.NewGuid(),
                Username = loginDataDto.Username,
                Password = loginDataDto.Password,
            };

            _context.Add(loginData);
            await _context.SaveChangesAsync();

            loginDataDto.Id = loginData.Id;

            return loginDataDto;
        }

        public string CreateSHA512(string strData)
        {
            var message = Encoding.UTF8.GetBytes(strData);
            using var alg = SHA512.Create();
            string hex = "";

            var hashValue = alg.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += string.Format("{0:x2}", x);
            }
            return hex;
        }
    }
}
